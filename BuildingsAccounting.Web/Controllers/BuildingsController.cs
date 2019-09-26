using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuildingsAccounting.Web.Infrastructure;
using BuildingsInfo.EF.Models;
using BuildingsAccounting.Web.Models;

namespace BuildingsAccounting.Web.Controllers
{
    public class BuildingsController : Controller
    {
       
        public ViewResult Browse()
        {
            return View(ToTableModels(UowCreator.Uow.BuildingRepository.GetAll()));
        }

        public ViewResult Selection()
        {
            ViewBag.selTypeName = UowCreator.Uow.BuildingRepository.GetUsedTypeNames().Select(e => new SelectListItem
            {
                Text = e,
                Value = e
            }).ToList();
            return View(ToTableModels(UowCreator.Uow.BuildingRepository.GetAll()));
        }

        public PartialViewResult _SelectData(string selAddress, string selTypeName, 
            int? floorsNumberFrom, int? floorsNumberTo, double? areaFrom, double? areaTo)
        {
            var model = UowCreator.Uow.BuildingRepository.Filter(selAddress, selTypeName, 
                floorsNumberFrom, floorsNumberTo, areaFrom, areaTo);

            return PartialView("_ShowCards", ToTableModels(model));
        }

        public static IEnumerable<BuildingTableModel> ToTableModels(IEnumerable<Building> objects)
        {
            return objects.Select(e => (BuildingTableModel)e);
        }
    }
}