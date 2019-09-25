using System.Data.Entity;
using BuildingsInfo.EF.Models;
using BuildingsInfo.EF.ModelsConfig;

namespace BuildingsInfo.EF.DataContext
{
    public class BuildingsContext : DbContext
    {
        public BuildingsContext() : base("BuildingsConnection")
        {
            Database.SetInitializer(new ContextInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BuildingTypeConfiguration());
            modelBuilder.Configurations.Add(new BuildingConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Building> Buildings { get; set; }
        public DbSet<BuildingType> BuildingTypes { get; set; }
    }
}
