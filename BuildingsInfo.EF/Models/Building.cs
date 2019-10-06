using System;

namespace BuildingsInfo.EF.Models
{
    public class Building
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public int? FloorsNumber { get; set; }
        public double? Area { get; set; }
        public string Note { get; set; }
        public string Description { get; set; }

        public string[] Photos { get; set; }
        public string PhotosAsString
        {
            get
            {
                return Photos != null ? string.Join(",", Photos) : null;
            }

            set
            {
                Photos = value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        public int BuildingTypeId { get; set; }
        public BuildingType BuildingType { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
