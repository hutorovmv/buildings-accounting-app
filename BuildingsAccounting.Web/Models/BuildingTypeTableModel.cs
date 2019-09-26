using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using BuildingsInfo.EF.Models;
using BuildingsAccounting.Web.Infrastructure;

namespace BuildingsAccounting.Web.Models
{
    public class BuildingTypeTableModel
    {
        public int Id { get; set; }

        [Display(Name = "Назва")]
        public string Name { get; set; }
        
        [Display(Name = "Назва батьківського типу")]
        public string ParentName { get; set; }

        public static explicit operator BuildingTypeTableModel(BuildingType obj)
        {
            return new BuildingTypeTableModel
            {
                Id = obj.Id,
                Name = obj.Name,
                ParentName = obj.ParentId.HasValue ? UowCreator.Uow.BuildingTypeRepository.GetName(obj.ParentId.Value) : "(інформація відсутня)"
            };
        }
    }
}