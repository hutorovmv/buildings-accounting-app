using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuildingsInfo.EF.Models;
using BuildingsAccounting.Web.Infrastructure;
using BuildingsAccounting.Web.Models;
using BuildingsInfo.EF.Repositories.Interfaces;

namespace BuildingsAccounting.Web.Controllers
{
    public class BuildingTypesController : Controller
    {
        private static IBuildingsInfoUOW uow = UowCreator.Uow;
        private IBuildingTypeRepository repository = uow.BuildingTypeRepository;

        public ViewResult Browse()
        {
            return View(ToTableModels(repository.GetAll()));
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
            return View(ToTableModels(repository.GetAll()));
        }

        public PartialViewResult _SelectData(string selName, string selParentType)
        {
            var model = repository.Filter(selName, selParentType);

            return PartialView("_ShowCards", ToTableModels(model));
        }

        public static IEnumerable<BuildingTypeTableModel> ToTableModels(IEnumerable<BuildingType> objects)
        {
            return objects.Select(e => (BuildingTypeTableModel)e);
        }
    }
}