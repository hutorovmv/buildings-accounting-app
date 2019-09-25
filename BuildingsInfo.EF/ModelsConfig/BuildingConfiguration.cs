using System.Data.Entity.ModelConfiguration;
using BuildingsInfo.EF.Models;

namespace BuildingsInfo.EF.ModelsConfig
{
    public class BuildingConfiguration : EntityTypeConfiguration<Building>
    {
        public BuildingConfiguration()
        {
            HasKey(p => p.Id);

            Property(p => p.Address).IsRequired().IsUnicode().HasMaxLength(100);
            Property(p => p.Note).IsUnicode();
            Property(p => p.Description).IsUnicode();
            Property(p => p.PhotosAsString).IsOptional().IsUnicode();
        }
    }
}
