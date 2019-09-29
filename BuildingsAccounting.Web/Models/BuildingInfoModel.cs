using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BuildingsInfo.EF.Models;

namespace BuildingsAccounting.Web.Models
{
    public class BuildingInfoModel
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public int? FloorsNumber { get; set; }
        public double? Area { get; set; }
        public int BuildingTypeId {get; set;}
        public string BuildingTypeName { get; set; }
        public string[] Photos { get; set; }
        public string[] NoteParagraphs { get; set; }
        public string[] DescriptionParagraphs { get; set; }

        public static explicit operator BuildingInfoModel(Building obj)
        {
            return new BuildingInfoModel
            {
                Id = obj.Id,
                Address = obj.Address,
                FloorsNumber = obj.FloorsNumber,
                Area = obj.Area,
                BuildingTypeId = obj.BuildingTypeId,
                BuildingTypeName = obj.BuildingType?.Name ?? "(тип не вказано)",
                Photos = obj.Photos?.Select(e => HttpContext.Current.Application["ImagesPath"] + e).ToArray(),
                NoteParagraphs = StringToArray(obj.Note),
                DescriptionParagraphs = StringToArray(obj.Description),
            };
        }

        public static string[] StringToArray(string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return null;
            return str.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}