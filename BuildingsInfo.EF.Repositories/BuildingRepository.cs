using BuildingsInfo.EF.DataContext;
using BuildingsInfo.EF.Models;
using BuildingsInfo.EF.Repositories.Interfaces;

namespace BuildingsInfo.EF.Repositories
{
    public class BuildingRepository : GenericEFRepository<Building>, IBuildingRepository
    {
        public BuildingRepository(BuildingsContext context) : base(context) { }

        // ...
    }
}
