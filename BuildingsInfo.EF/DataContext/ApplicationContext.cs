using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using BuildingsInfo.EF.Interfaces;
using BuildingsInfo.EF.Models;
using BuildingsInfo.EF.ModelsConfig;

namespace BuildingsInfo.EF.DataContext
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>, IBuildingsContext
    {
        public ApplicationContext() : base("BuildingsConnection") {
            //Database.SetInitializer(new ContextInitializer<ApplicationContext>());
        }

        public DbSet<Building> Buildings { get; set; }
        public DbSet<BuildingType> BuildingTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BuildingTypeConfiguration());
            modelBuilder.Configurations.Add(new BuildingConfiguration());
            modelBuilder.Configurations.Add(new ApplicationUserConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }
    }
}