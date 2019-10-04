using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using BuildingsInfo.EF.Models;

namespace BuildingsAccounting.Web.Models
{
    public class BuildingEditingModel
    {
        public int Id { get; set; }

        [Display(Name = "Адреса")]
        [Required(ErrorMessage = "Потрібно вказати адресу")]
        [StringLength(100, MinimumLength = 15, ErrorMessage = "Від 15 до 100 символів")]        
        public string Address { get; set; }

        [Display(Name = "Тип будівлі")]
        [Required(ErrorMessage = "Потрібно вказати тип")]
        [StringLength(45, MinimumLength = 3, ErrorMessage = "Від 3 до 45 символів")]
        public string BuildingTypeName { get; set; }

        [Display(Name = "Кількість поверхів")]
        [Range(1, 150, ErrorMessage = "Від 3 до 150")]
        public int? FloorsNumber { get; set; }

        [Display(Name = "Площа")]
        [Range(1, 15000, ErrorMessage = "Від 1 до 15000")]
        public double? Area { get; set; }

        [Display(Name = "Примітка")]
        [DataType(DataType.MultilineText)]
        public string Note { get; set; }

        [Display(Name = "Опис")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string[] Photos { get; set; }

        public IEnumerable<HttpPostedFileBase> Files { get; set; }

        public static explicit operator BuildingEditingModel(Building obj)
        {
            return new BuildingEditingModel
            {
                Id = obj.Id,
                Address = obj.Address,
                BuildingTypeName = obj.BuildingType?.Name,
                FloorsNumber = obj.FloorsNumber,
                Area = obj.Area,
                Note = obj.Note,
                Description = obj.Description,
                Photos = obj?.Photos
            };
        }
    }
}