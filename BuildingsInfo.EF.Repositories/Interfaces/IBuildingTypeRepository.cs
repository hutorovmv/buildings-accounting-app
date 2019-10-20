using System.Collections.Generic;
using BuildingsInfo.EF.Models;
using Common.Repositories.Interfaces;

namespace BuildingsInfo.EF.Repositories.Interfaces
{
    public interface IBuildingTypeRepository : IRepository<BuildingType>
    {
        string GetName(int id);
        BuildingType GetByName(string name);
        IEnumerable<string> GetExistingParentTypes();
        IEnumerable<BuildingType> Filter(string name, string parentName);
        IEnumerable<(string Name, int Id)> GetItemsTypeHierarchy(int id);
        IEnumerable<BuildingType> GetUserBuildingTypes(string UserId);
    }
}
