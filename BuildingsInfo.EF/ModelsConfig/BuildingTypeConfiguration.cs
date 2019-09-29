using System.Data.Entity.ModelConfiguration;
using BuildingsInfo.EF.Models;

namespace BuildingsInfo.EF.ModelsConfig
{
    public class BuildingTypeConfiguration : EntityTypeConfiguration<BuildingType>
    {
        public BuildingTypeConfiguration()
        {
            HasKey(p => p.Id);

            Property(p => p.Name).IsRequired().IsUnicode();
            Property(p => p.Description).IsUnicode();

            HasMany(p => p.Buildings)
                .WithRequired(p => p.BuildingType)
                .HasForeignKey(p => p.BuildingTypeId);

            HasOptional(p => p.Parent)
                .WithMany(p => p.Children)
                .HasForeignKey(p => p.ParentId)
                .WillCascadeOnDelete(false);
        }
    }
}
