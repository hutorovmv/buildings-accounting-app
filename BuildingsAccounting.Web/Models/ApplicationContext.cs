using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using BuildingsInfo.EF.Interfaces;
using BuildingsInfo.EF.Models;
using BuildingsInfo.EF.ModelsConfig;
using BuildingsInfo.EF.DataContext;

namespace BuildingsAccounting.Web.Models
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>, IBuildingsContext
    {
        public ApplicationContext() : base("BuildingsConnection") {
            Database.SetInitializer(new ContextInitializer<ApplicationContext>());
        }

        public DbSet<Building> Buildings { get; set; }
        public DbSet<BuildingType> BuildingTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BuildingTypeConfiguration());
            modelBuilder.Configurations.Add(new BuildingConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }
    }
}