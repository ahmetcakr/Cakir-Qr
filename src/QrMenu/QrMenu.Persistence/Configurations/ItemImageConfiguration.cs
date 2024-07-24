using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QrMenu.Domain.Entities;

namespace QrMenu.Persistence.Configurations;

public class ItemImageConfiguration : IEntityTypeConfiguration<ItemImage>
{
    public void Configure(EntityTypeBuilder<ItemImage> builder)
    {
        builder.ToTable("ItemImages");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.ItemId)
            .IsRequired();

        builder.Property(e => e.Image)
            .IsRequired();

        builder.Property(e => e.Description)
            .HasMaxLength(200);
    }
}
