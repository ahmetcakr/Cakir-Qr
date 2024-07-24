using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Cryptography;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.OpenSsl;
using System.Net;

namespace Core.Mailing.MailKitImplementations;

public class MailKitMailService : IMailService
{
    private readonly MailSettings _mailSettings;
    private DkimSigner? _signer;

    public MailKitMailService(IConfiguration configuration)
    {
        const string configurationSection = "MailSettings";
        _mailSettings =
            configuration.GetSection(configurationSection).Get<MailSettings>()
            ?? throw new NullReferenceException($"\"{configurationSection}\" section cannot found in configuration.");
        _signer = null;
    }

    public void SendMail(Mail mail)
    {
        if (mail.ToList == null || mail.ToList.Count < 1)
            return;
        EmailPrepare(mail, email: out MimeMessage email, smtp: out SmtpClient smtp);
        smtp.Send(email);
        smtp.Disconnect(true);
        email.Dispose();
        smtp.Dispose();
    }

    public async Task SendEmailAsync(Mail mail)
    {
        if (mail.ToList == null || mail.ToList.Count < 1)
            return;
        EmailPrepare(mail, email: out MimeMessage email, smtp: out SmtpClient smtp);
        await smtp.SendAsync(email);
        smtp.Disconnect(true);
        email.Dispose();
        smtp.Dispose();
    }

    private void EmailPrepare(Mail mail, out MimeMessage email, out SmtpClient smtp)
    {
        email = new MimeMessage();
        email.From.Add(new MailboxAddress(_mailSettings.SenderFullName, _mailSettings.SenderEmail));
        email.To.AddRange(mail.ToList);
        if (mail.CcList != null && mail.CcList.Any())
            email.Cc.AddRange(mail.CcList);
        if (mail.BccList != null && mail.BccList.Any())
            email.Bcc.AddRange(mail.BccList);

        email.Subject = mail.Subject;
        if (mail.UnsubscribeLink != null)
            email.Headers.Add("List-Unsubscribe", $"<{mail.UnsubscribeLink}>");

        // Check the body content
        if (string.IsNullOrEmpty(mail.TextBody) && string.IsNullOrEmpty(mail.HtmlBody))
        {
            throw new ArgumentException("Both TextBody and HtmlBody are empty. The email body cannot be empty.");
        }

        BodyBuilder bodyBuilder = new BodyBuilder
        {
            TextBody = mail.TextBody,
            HtmlBody = string.IsNullOrEmpty(mail.HtmlBody) ? System.Net.WebUtility.HtmlEncode(mail.TextBody) : mail.HtmlBody
        };

        if (mail.Attachments != null)
        {
            foreach (MimeEntity attachment in mail.Attachments)
            {
                if (attachment != null)
                {
                    bodyBuilder.Attachments.Add(attachment);
                }
            }
        }

        // Log the body before setting it to the email
        Console.WriteLine("TextBody: " + bodyBuilder.TextBody);
        Console.WriteLine("HtmlBody: " + bodyBuilder.HtmlBody);

        email.Body =  bodyBuilder.ToMessageBody();

        // Log the final email body
        Console.WriteLine("Final Email Body: " + email.Body.ToString());

        email.Prepare(EncodingConstraint.SevenBit);



        if (!string.IsNullOrEmpty(_mailSettings.DkimPrivateKey) &&
            !string.IsNullOrEmpty(_mailSettings.DkimSelector) &&
            !string.IsNullOrEmpty(_mailSettings.DomainName))
        {
            _signer = new DkimSigner(ReadPrivateKeyFromPemEncodedString(), _mailSettings.DomainName, _mailSettings.DkimSelector)
            {
                HeaderCanonicalizationAlgorithm = DkimCanonicalizationAlgorithm.Simple,
                BodyCanonicalizationAlgorithm = DkimCanonicalizationAlgorithm.Simple,
                AgentOrUserIdentifier = $"@{_mailSettings.DomainName}",
                QueryMethod = "dns/txt"
            };

            HeaderId[] headers = { HeaderId.From, HeaderId.Subject, HeaderId.To };
            _signer.Sign(email, headers);
        }

        var credentials = new NetworkCredential(_mailSettings.UserName, _mailSettings.Password);

        smtp = new SmtpClient();
        smtp.Connect(_mailSettings.Server, _mailSettings.Port);
        if (_mailSettings.AuthenticationRequired)
        {
            smtp.Authenticate(credentials);
        }
    }

    private AsymmetricKeyParameter ReadPrivateKeyFromPemEncodedString()
    {
        AsymmetricKeyParameter result;
        string pemEncodedKey = "-----BEGIN RSA PRIVATE KEY-----\n" + _mailSettings.DkimPrivateKey + "\n-----END RSA PRIVATE KEY-----";
        using (StringReader stringReader = new(pemEncodedKey))
        {
            PemReader pemReader = new(stringReader);
            object? pemObject = pemReader.ReadObject();
            result = ((AsymmetricCipherKeyPair)pemObject).Private;
        }

        return result;
    }
}
