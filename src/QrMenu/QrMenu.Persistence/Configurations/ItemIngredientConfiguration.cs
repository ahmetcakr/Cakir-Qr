using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QrMenu.Domain.Entities;

namespace QrMenu.Persistence.Configurations;

public class ItemIngredientConfiguration : IEntityTypeConfiguration<ItemIngredient>
{
    public void Configure(EntityTypeBuilder<ItemIngredient> builder)
    {
        builder.ToTable("ItemIngredients");

        builder.HasKey(e => e.Id);

        builder.HasOne(e => e.Item)
            .WithOne(e => e.ItemIngredient)
            .HasForeignKey<ItemIngredient>(e => e.ItemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(e => e.ItemId)
            .IsRequired();
    }
}
