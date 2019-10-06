using System.Data.Entity.ModelConfiguration;
using BuildingsInfo.EF.Models;

namespace BuildingsInfo.EF.ModelsConfig
{
    public class ApplicationUserConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfiguration()
        {
            HasMany(p => p.Buildings)
                .WithRequired(p => p.User)
                .HasForeignKey(p => p.UserId)
                .WillCascadeOnDelete(false);

            HasMany(p => p.BuildingTypes)
                .WithRequired(p => p.User)
                .HasForeignKey(p => p.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}
