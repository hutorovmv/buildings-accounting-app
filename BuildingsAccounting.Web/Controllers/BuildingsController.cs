using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuildingsAccounting.Web.Infrastructure;
using BuildingsInfo.EF.Models;
using BuildingsAccounting.Web.Models;
using BuildingsInfo.EF.Repositories.Interfaces;
using BuildingsAccounting.Web.Pagination;

namespace BuildingsAccounting.Web.Controllers
{
    public class BuildingsController : Controller
    {
        const int PAGE_SIZE = 2;

        private static IBuildingsInfoUOW uow = UowCreator.Uow;
        private IBuildingRepository repository = uow.BuildingRepository;

        public ViewResult Browse(int index = 1)
        {
            return View(ToTableModels(repository.GetAll()).ToPagedList(PAGE_SIZE, index));
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
            return View(ToTableModels(repository.GetAll()).ToPagedList(PAGE_SIZE, 1));
        }

        public PartialViewResult _SelectData(string selAddress, string selTypeName, 
            int? floorsNumberFrom, int? floorsNumberTo, double? areaFrom, double? areaTo, int index)
        {
            var model = UowCreator.Uow.BuildingRepository.Filter(selAddress, selTypeName, 
                floorsNumberFrom, floorsNumberTo, areaFrom, areaTo);

            return PartialView("_ShowCards", ToTableModels(model).ToPagedList(PAGE_SIZE, index));
        }

        public static IEnumerable<BuildingTableModel> ToTableModels(IEnumerable<Building> objects)
        {
            return objects.Select(e => (BuildingTableModel)e);
        }
    }
}