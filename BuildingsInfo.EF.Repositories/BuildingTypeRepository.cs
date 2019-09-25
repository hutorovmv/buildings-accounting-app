using BuildingsInfo.EF.DataContext;
using BuildingsInfo.EF.Models;
using BuildingsInfo.EF.Repositories.Interfaces;

namespace BuildingsInfo.EF.Repositories
{
    public class BuildingTypeRepository : GenericEFRepository<BuildingType>, IBuildingTypeRepository
    {
        public BuildingTypeRepository(BuildingsContext context) : base(context) { }

        // ...
    }
}
