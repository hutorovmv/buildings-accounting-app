using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuildingsInfo.EF.Models;
using BuildingsAccounting.Web.Infrastructure;
using BuildingsAccounting.Web.Models;

namespace BuildingsAccounting.Web.Controllers
{
    public class BuildingTypesController : Controller
    {
        public ViewResult Browse()
        {
            return View(ToTableModels(UowCreator.Uow.BuildingTypeRepository.GetAll()));
        }

        public ViewResult Selection()
        {
            ViewBag.selParentType = UowCreator.Uow.BuildingTypeRepository.GetExistingParentTypes().Select(e => new SelectListItem
            {
                Text = e,
                Value = e
            }).ToList();
            return View(ToTableModels(UowCreator.Uow.BuildingTypeRepository.GetAll()));
        }

        public PartialViewResult _SelectData(string selName, string selParentType)
        {
            var model = UowCreator.Uow.BuildingTypeRepository.Filter(selName, selParentType);

            return PartialView("_ShowCards", ToTableModels(model));
        }

        public static IEnumerable<BuildingTypeTableModel> ToTableModels(IEnumerable<BuildingType> objects)
        {
            return objects.Select(e => (BuildingTypeTableModel)e);
        }
    }
}