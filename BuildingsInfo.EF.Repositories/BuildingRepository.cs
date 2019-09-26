using System.Linq;
using System.Collections.Generic;
using BuildingsInfo.EF.DataContext;
using BuildingsInfo.EF.Models;
using BuildingsInfo.EF.Repositories.Interfaces;

namespace BuildingsInfo.EF.Repositories
{
    public class BuildingRepository : GenericEFRepository<Building>, IBuildingRepository
    {
        public BuildingRepository(BuildingsContext context) : base(context) { }

        public IEnumerable<Building> Filter(string address, string typeName, int? floorsFrom, int? floorsTo, double? areaFrom, double? areaTo)
        {
            var model = GetAll();

            if (!string.IsNullOrWhiteSpace(address))
                model = model.Where(p => p.Address.ToLower().Contains(address.ToLower()));

            if (!string.IsNullOrWhiteSpace(typeName) && typeName != "...")
                model = model.Where(p => p.BuildingType?.Name == typeName);

            if (floorsFrom.HasValue)
                model = model.Where(p => p.FloorsNumber >= floorsFrom);
            if (floorsTo.HasValue)
                model = model.Where(p => p.FloorsNumber <= floorsTo);

            if (areaFrom.HasValue)
                model = model.Where(p => p.Area >= areaFrom.Value);
            if (areaTo.HasValue)
                model = model.Where(p => p.Area <= areaTo.Value);

            return model;
        }

        public IEnumerable<string> GetUsedTypeNames()
        {
            List<string> typeNames = new List<string>();
            typeNames.Add("...");
            typeNames.AddRange(GetAll()
                     .Where(p => p.BuildingType != null)
                     .Select(p => p.BuildingType.Name)
                     .Distinct()
                     .OrderBy(e => e)
                     .ToList());
            return typeNames;
        }
    }
}
