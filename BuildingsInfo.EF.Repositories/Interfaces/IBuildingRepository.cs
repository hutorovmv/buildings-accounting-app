using System.Collections.Generic;
using BuildingsInfo.EF.Models;
using Common.Repositories.Interfaces;

namespace BuildingsInfo.EF.Repositories.Interfaces
{
    public interface IBuildingRepository : IRepository<Building>
    {
        IEnumerable<Building> Filter(string address, string typeName, int? floorsFrom, int? floorsTo, double? areaFrom, double? areaTo);
        IEnumerable<string> GetUsedTypeNames();
        IEnumerable<Building> GetUserBuildings(string UserId);
    }
}
