using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BuildingsInfo.EF.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Building> Buildings { get; set; }
        public ICollection<BuildingType> BuildingTypes { get; set; }
    }
}