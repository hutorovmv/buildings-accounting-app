using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuildingsAccounting.Web.Infrastructure;
using BuildingsInfo.EF.Models;
using BuildingsAccounting.Web.Models;
using BuildingsInfo.EF.Repositories.Interfaces;

namespace BuildingsAccounting.Web.Controllers
{
    public class BuildingsController : Controller
    {
        private static IBuildingsInfoUOW uow = UowCreator.Uow;
        private IBuildingRepository repository = uow.BuildingRepository;

        public ViewResult Browse()
        {
            return View(ToTableModels(repository.GetAll()));
        }

        public ViewResult Building(int id)
        {
            return View((BuildingInfoModel)repository.Get(id));
        }

        public ViewResult Selection()
        {
            ViewBag.selTypeName = repository.GetUsedTypeNames().Select(e => new SelectListItem
            {
                Text = e,
                Value = e
            }).ToList();
            return View(ToTableModels(repository.GetAll()));
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