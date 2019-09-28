using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BuildingsInfo.EF.Models;

namespace BuildingsAccounting.Web.Models
{
    public class BuildingTypeInfoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentTypeId { get; set; }
        public string ParentTypeName { get; set; }
        public string[] DescriptionParagraphs { get; set; }

        public static explicit operator BuildingTypeInfoModel(BuildingType obj)
        {
            return new BuildingTypeInfoModel
            {
                Id = obj.Id,
                Name = obj.Name,
                ParentTypeId = obj.ParentId.HasValue ? obj.ParentId.Value : (int?)null,
                ParentTypeName = obj.Parent?.Name ?? "(тип не вказано)",
                DescriptionParagraphs = obj.Description?.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
            };
        }
    }
}