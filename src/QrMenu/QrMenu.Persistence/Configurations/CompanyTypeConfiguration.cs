using QrMenu.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QrMenu.Persistence.Configurations;

public class CompanyTypeConfiguration : IEntityTypeConfiguration<CompanyType>
{
    public void Configure(EntityTypeBuilder<CompanyType> builder)
    {
        builder.ToTable("CompanyTypes").HasKey(c => c.Id);

        builder.Property(c => c.TypeName).HasColumnName("TypeName").IsRequired();

        builder.HasMany(c => c.Companies)
            .WithOne(c => c.CompanyType)
            .HasForeignKey(c => c.CompanyTypeId);

        builder.HasData(getSeeds());
    }

    private IEnumerable<CompanyType> getSeeds()
    {
        List<CompanyType> companyTypes = new();

        CompanyType adminCompanyType = new()
        {
            Id = 1,
            TypeName = "Admin",
            Description = "Admin Company Type"
        };

        companyTypes.Add(adminCompanyType);

        return companyTypes.ToArray();
    }
}
