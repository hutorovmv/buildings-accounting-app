using System;
using System.Web;
using System.ComponentModel.DataAnnotations;
using BuildingsInfo.EF.Models;
using System.IO;
using BuildingsAccounting.Web.Infrastructure;

namespace BuildingsAccounting.Web.Models
{
    public class BuildingTableModel : IComparable<BuildingTableModel>, IComparable
    {
        public int Id { get; set; }
        
        [Display(Name = "Адреса")]
        public string Address { get; set; }

        [Display(Name = "Назва типу будівлі")]
        public string BuildingTypeName { get; set; }

        [Display(Name = "Кількість поверхів")]
        public int? FloorsNumber { get; set; }

        [Display(Name = "Площа")]
        public double? Area { get; set; }

        [Display(Name = "Фото")]  
        public string Image { get; set; }

        public int CompareTo(BuildingTableModel obj)
        {
            return this.Address.CompareTo(obj.Address);
        }

        public int CompareTo(object obj)
        {
            return this.CompareTo((BuildingTableModel)obj);
        }

        public static explicit operator BuildingTableModel(Building obj)
        {
            return new BuildingTableModel
            {
                Id = obj.Id,
                Address = obj.Address,
                BuildingTypeName = UowCreator.Uow.BuildingTypeRepository.GetName(obj.BuildingTypeId),
                FloorsNumber = obj.FloorsNumber,
                Area = obj.Area,
                Image = obj.Photos.Length > 0 ? obj.Photos[0] : HttpContext.Current.Application["DefaultImage"].ToString()
            };
        }
    }
}