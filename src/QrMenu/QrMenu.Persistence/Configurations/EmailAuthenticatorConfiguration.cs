using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QrMenu.Persistence.Configurations;

public class EmailAuthenticatorConfiguration : IEntityTypeConfiguration<EmailAuthenticator>
{
    public void Configure(EntityTypeBuilder<EmailAuthenticator> builder)
    {
        builder.ToTable("EmailAuthenticators").HasKey(ea => ea.Id);

        builder.Property(ea => ea.Id).HasColumnName("Id").IsRequired();
        builder.Property(ea => ea.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(ea => ea.ActivationKey).HasColumnName("ActivationKey");
        builder.Property(ea => ea.IsVerified).HasColumnName("IsVerified").IsRequired();
        builder.Property(ea => ea.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(ea => ea.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(ea => ea.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(ea => !ea.DeletedDate.HasValue);

        builder.HasOne(ea => ea.User);

        builder.HasData(getSeeds());
    }


    private IEnumerable<EmailAuthenticator> getSeeds()
    {
        List<EmailAuthenticator> emailAuthenticators = new();

        EmailAuthenticator emailAuthenticator = new()
        {
            Id = 1,
            UserId = 1,
            ActivationKey = "",
            IsVerified = true,
            CreatedDate = DateTime.Now,
            UpdatedDate = null,
            DeletedDate = null
        };

        emailAuthenticators.Add(emailAuthenticator);
        return emailAuthenticators;
    }
}
