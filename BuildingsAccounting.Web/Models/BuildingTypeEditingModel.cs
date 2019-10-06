using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using BuildingsInfo.EF.Models;

namespace BuildingsAccounting.Web.Models
{
    public class BuildingTypeEditingModel
    {
        public int Id { get; set; }

        [Display(Name = "Назва")]
        [Required(ErrorMessage = "Потрібно вказати назву")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Від 3 до 60 символів")]
        public string Name { get; set; }

        [Display(Name = "Тип вищого рівня")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Від 3 до 60 символів")]
        public string ParentName { get; set; }

        [Display(Name = "Опис")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string UserId { get; set; }

        public static explicit operator BuildingTypeEditingModel(BuildingType obj)
        {
            return new BuildingTypeEditingModel
            {
                Id = obj.Id,
                Name = obj.Name,
                ParentName = obj.Parent?.Name,
                Description = obj.Description,
                UserId = obj.UserId
            };
        }
    }
}