using BuildingsInfo.EF.Models;
using Common.Repositories.Interfaces;

namespace BuildingsInfo.EF.Repositories.Interfaces
{
    public interface IBuildingTypeRepository : IRepository<BuildingType>
    {
        string GetName(int id);
    }
}
