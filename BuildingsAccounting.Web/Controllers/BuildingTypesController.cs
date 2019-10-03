using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuildingsInfo.EF.Models;
using BuildingsAccounting.Web.Infrastructure;
using BuildingsAccounting.Web.Models;
using BuildingsInfo.EF.Repositories.Interfaces;
using BuildingsAccounting.Web.Pagination;

namespace BuildingsAccounting.Web.Controllers
{
    public class BuildingTypesController : Controller
    {
        const int PAGE_SIZE = 2;

        private static IBuildingsInfoUOW uow = UowCreator.Uow;
        private IBuildingTypeRepository repository = uow.BuildingTypeRepository;

        public ViewResult Browse(int index = 1)
        {
            return View(ToTableModels(repository.GetAll()).ToPagedList(PAGE_SIZE, index));
        }

        public ViewResult BuildingType(int id)
        {
            return View((BuildingTypeInfoModel)repository.Get(id));
        }

        public ViewResult Selection()
        {
            ViewBag.selParentType = repository.GetExistingParentTypes().Select(e => new SelectListItem
            {
                Text = e,
                Value = e
            }).ToList();
            return View(ToTableModels(repository.GetAll()).ToPagedList(PAGE_SIZE, 1));
        }

        public PartialViewResult _SelectData(string selName, string selParentType, int index)
        {
            var model = repository.Filter(selName, selParentType);

            return PartialView("_ShowCards", ToTableModels(model).ToPagedList(PAGE_SIZE, index));
        }

        public static IEnumerable<BuildingTypeTableModel> ToTableModels(IEnumerable<BuildingType> objects)
        {
            return objects.Select(e => (BuildingTypeTableModel)e);
        }
    }
}