using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QrMenu.Domain.Entities;

namespace QrMenu.Persistence.Configurations;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable("Items");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.ItemName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.Description)
            .HasMaxLength(200);

        builder.Property(e => e.Price)
            .HasColumnType("decimal(18,2)");

        builder.HasOne(e => e.Category)
            .WithMany(e => e.Items)
            .HasForeignKey(e => e.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.ItemImages)
            .WithOne(e => e.Item)
            .HasForeignKey(e => e.ItemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.ItemIngredient)
            .WithOne(e => e.Item)
            .HasForeignKey<ItemIngredient>(e => e.ItemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(e => e.IsAvailable)
            .HasDefaultValue(true);
    }
}
