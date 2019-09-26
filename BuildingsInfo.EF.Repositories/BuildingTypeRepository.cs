﻿using System.Linq;
using System.Collections.Generic;
using BuildingsInfo.EF.DataContext;
using BuildingsInfo.EF.Models;
using BuildingsInfo.EF.Repositories.Interfaces;

namespace BuildingsInfo.EF.Repositories
{
    public class BuildingTypeRepository : GenericEFRepository<BuildingType>, IBuildingTypeRepository
    {
        public BuildingTypeRepository(BuildingsContext context) : base(context) { }

        public string GetName(int id)
        {
            return Get(id).Name;
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
    }
}
