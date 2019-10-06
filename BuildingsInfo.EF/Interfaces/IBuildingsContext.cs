using System.Data.Entity;
using BuildingsInfo.EF.Models;

namespace BuildingsInfo.EF.Interfaces
{
    public interface IBuildingsContext
    {
        DbSet<Building> Buildings { get; set; }
        DbSet<BuildingType> BuildingTypes { get; set; }
    }
}
