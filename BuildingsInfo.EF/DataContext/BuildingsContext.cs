using System.Data.Entity;
using BuildingsInfo.EF.Models;
using BuildingsInfo.EF.ModelsConfig;
using BuildingsInfo.EF.Interfaces;

namespace BuildingsInfo.EF.DataContext
{
    public class BuildingsContext : DbContext, IBuildingsContext
    {
        public BuildingsContext() : base("BuildingsConnection")
        {
            Database.SetInitializer(new ContextInitializer<BuildingsContext>());
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
