using QrMenu.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Security.Entities;
using Core.Security.Hashing;

namespace QrMenu.Persistence.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Companies").HasKey(c => c.Id);

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("Id").IsRequired();
        builder.Property(c => c.CompanyName).HasColumnName("CompanyName").IsRequired();
        builder.Property(c => c.CompanyTypeId).HasColumnName("CompanyTypeId").IsRequired();

        builder.HasOne(c => c.CompanyType)
            .WithMany()
            .HasForeignKey(c => c.CompanyTypeId);

        builder.HasData(getSeeds());
    }

    private IEnumerable<Company> getSeeds()
    {
        List<Company> companies = new();

        Company adminCompany = new()
        {
            Id = 1,
            CompanyName = "Admin Company",
            CompanyTypeId = 1
        };

        companies.Add(adminCompany);

        return companies.ToArray();
    }
}
