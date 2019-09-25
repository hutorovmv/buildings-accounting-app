using System.Collections.Generic;

namespace BuildingsInfo.EF.Models
{
    public class BuildingType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Building> Buildings { get; set; }

        public int? ParentId { get; set; }
        public BuildingType Parent { get; set; }
        public ICollection<BuildingType> Children { get; set; }
    }
}
