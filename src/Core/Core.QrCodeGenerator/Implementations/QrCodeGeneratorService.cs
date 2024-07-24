using Core.QrCodeGenerator.Services;
using QRCoder;

namespace Core.QrCodeGenerator.Implementations;
public class QrCodeGeneratorService : IQrCodeGeneratorService
{
    public Task<byte[]> GenerateQrCodeAsync(string text)
    {
        var qrGenerator = new QRCodeGenerator();
        var qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);

        var qrCode = new PngByteQRCode(qrCodeData);

        var qrCodeImage = qrCode.GetGraphic(20);

        return Task.FromResult(qrCodeImage);
    }
}
