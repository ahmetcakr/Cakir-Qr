using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QrMenu.Domain.Entities;

namespace QrMenu.Persistence.Configurations;

public class MenuQrCodeConfiguration : IEntityTypeConfiguration<MenuQrCode>
{
    public void Configure(EntityTypeBuilder<MenuQrCode> builder)
    {
        builder.ToTable("MenuQrCodes");

        builder.HasKey(e => e.Id);

        builder.Property(x=>x.QrCodeText)
            .IsRequired();

        builder.Property(e => e.QrCode)
            .IsRequired();

        builder.HasOne(e => e.Menu)
            .WithMany(e => e.MenuQrCodes)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(e => e.MenuId)
            .IsRequired();
    }
}
