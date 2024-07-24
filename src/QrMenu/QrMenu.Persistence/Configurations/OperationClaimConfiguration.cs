using Core.Security.Constants;
using Core.Security.Entities;
using QrMenu.Application.Features.Users.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QrMenu.Persistence.Configurations;

public class OperationClaimConfiguration : IEntityTypeConfiguration<OperationClaim>
{
    public void Configure(EntityTypeBuilder<OperationClaim> builder)
    {
        builder.ToTable("OperationClaims").HasKey(oc => oc.Id);

        builder.Property(oc => oc.Id).HasColumnName("Id").IsRequired();
        builder.Property(oc => oc.Name).HasColumnName("Name").IsRequired();
        builder.Property(oc => oc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(oc => oc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(oc => oc.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(oc => !oc.DeletedDate.HasValue);

        builder.HasMany(oc => oc.UserOperationClaims);

        builder.HasData(getSeeds());
    }

    private HashSet<OperationClaim> getSeeds()
    {
        int id = 0;
        HashSet<OperationClaim> seeds =
            new()
            {
            new OperationClaim { Id = ++id, Name = GeneralOperationClaims.Admin }
            };


        #region Users

        seeds.Add(new OperationClaim { Id = ++id, Name = "users.admin" });

        seeds.Add(new OperationClaim { Id = ++id, Name = "users.read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "users.write" });

        seeds.Add(new OperationClaim { Id = ++id, Name = "users.add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "users.update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "users.delete" });

        #endregion

        #region OperationClaims

        seeds.Add(new OperationClaim { Id = ++id, Name = "operationclaims.admin" });

        seeds.Add(new OperationClaim { Id = ++id, Name = "operationclaims.read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "operationclaims.write" });

        seeds.Add(new OperationClaim { Id = ++id, Name = "operationclaims.add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "operationclaims.update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "operationclaims.delete" });

        #endregion

        #region UserOperationClaims

        seeds.Add(new OperationClaim { Id = ++id, Name = "useroperationclaims.admin" });

        seeds.Add(new OperationClaim { Id = ++id, Name = "useroperationclaims.read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "useroperationclaims.write" });

        seeds.Add(new OperationClaim { Id = ++id, Name = "useroperationclaims.add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "useroperationclaims.update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "useroperationclaims.delete" });

        #endregion

        #region Companies

        seeds.Add(new OperationClaim { Id = ++id, Name = "companies.admin" });

        seeds.Add(new OperationClaim { Id = ++id, Name = "companies.read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "companies.write" });

        seeds.Add(new OperationClaim { Id = ++id, Name = "companies.add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "companies.update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "companies.delete" });

        #endregion

        #region CompanyTypes

        seeds.Add(new OperationClaim { Id = ++id, Name = "companytypes.admin" });

        seeds.Add(new OperationClaim { Id = ++id, Name = "companytypes.read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "companytypes.write" });

        seeds.Add(new OperationClaim { Id = ++id, Name = "companytypes.add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "companytypes.update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "companytypes.delete" });

        #endregion

        #region Categories

        seeds.Add(new OperationClaim { Id = ++id, Name = "categories.admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "categories.read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "categories.write" });

        seeds.Add(new OperationClaim { Id = ++id, Name = "categories.add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "categories.update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "categories.delete" });

        #endregion

        #region Items

        seeds.Add(new OperationClaim { Id = ++id, Name = "items.admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "items.read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "items.write" });

        seeds.Add(new OperationClaim { Id = ++id, Name = "items.add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "items.update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "items.delete" });

        #endregion

        #region ItemImages

        seeds.Add(new OperationClaim { Id = ++id, Name = "itemimages.admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "itemimages.read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "itemimages.write" });

        seeds.Add(new OperationClaim { Id = ++id, Name = "itemimages.add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "itemimages.update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "itemimages.delete" });

        #endregion

        #region ItemIngredients

        seeds.Add(new OperationClaim { Id = ++id, Name = "itemingredients.admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "itemingredients.read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "itemingredients.write" });

        seeds.Add(new OperationClaim { Id = ++id, Name = "itemingredients.add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "itemingredients.update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "itemingredients.delete" });

        #endregion


        return seeds;
    }


}
