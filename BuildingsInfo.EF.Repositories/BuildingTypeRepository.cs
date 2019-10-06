using System.Linq;
using System.Collections.Generic;
using BuildingsInfo.EF.DataContext;
using BuildingsInfo.EF.Models;
using BuildingsInfo.EF.Repositories.Interfaces;
using System.Data.Entity;

namespace BuildingsInfo.EF.Repositories
{
    public class BuildingTypeRepository<Context> : GenericEFRepository<BuildingType>, IBuildingTypeRepository where Context : DbContext
    {
        public BuildingTypeRepository(Context context) : base(context) { }

        public string GetName(int id)
        {
            return Get(id).Name;
        }

        public BuildingType GetByName(string name)
        {
            return GetAll().FirstOrDefault(p => p.Name == name);
        }

        public IEnumerable<string> GetExistingParentTypes()
        {
            List<string> typeNames = new List<string>();
            typeNames.Add("...");
            typeNames.AddRange(GetAll()
                     .Where(p => p.Parent != null)
                     .Select(p => p.Parent.Name)
                     .Distinct()
                     .OrderBy(e => e)
                     .ToList());
            return typeNames;
        }

        public IEnumerable<BuildingType> Filter(string name, string parentName)
        {
            var model = GetAll();

            if (!string.IsNullOrWhiteSpace(name))
                model = model.Where(p => p.Name.ToLower().StartsWith(name.ToLower()));

            if (!string.IsNullOrWhiteSpace(parentName) && parentName != "...")
                model = model.Where(p => p.Parent?.Name == parentName);

            return model;
        }

        public IEnumerable<(string Name, int Id)> GetItemsTypeHierarchy(int id)
        {
            List<(string Name, int Id)> hierarchy = new List<(string Name, int Id)>();
            for (var item = Get(id); item != null; item = item.Parent)
            {
                hierarchy.Add((Name: item.Name, Id: item.Id));
            }

            hierarchy.Reverse();
            return hierarchy;
        }
    }
}
