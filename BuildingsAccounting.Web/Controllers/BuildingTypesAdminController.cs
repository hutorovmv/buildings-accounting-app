using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuildingsAccounting.Web.Models;
using BuildingsAccounting.Web.Infrastructure;
using BuildingsInfo.EF.Models;
using BuildingsInfo.EF.Repositories.Interfaces;

namespace BuildingsAccounting.Web.Controllers
{
    public class BuildingTypesAdminController : Controller
    {
        private static IBuildingsInfoUOW uow = UowCreator.Uow;
        private IBuildingTypeRepository repository = uow.BuildingTypeRepository;

        public ViewResult Create()
        {
            ViewBag.ParentName = SelectTypeNames(repository);
            return View(new BuildingTypeEditingModel());
        }

        [HttpPost]
        public ActionResult Create(BuildingTypeEditingModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ParentName = SelectTypeNames(repository);
                return View(model);
            }

            repository.Create(CreateBuildingTypeObject(model));
            uow.Save();

            return RedirectToAction("Create");
        }

        public ViewResult Edit(int id)
        {
            var model = (BuildingTypeEditingModel)repository.Get(id);
            ViewBag.ParentName = SelectTypeNames(repository);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(BuildingTypeEditingModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ParentName = SelectTypeNames(repository);
                return View(model);
            }

            BuildingType obj = CreateBuildingTypeObject(model);
            BuildingType item = repository.Get(obj.Id);

            item.Name = obj.Name;
            item.ParentId = obj.ParentId;
            item.Parent = obj.Parent;
            item.Description = obj.Description;

            repository.Update(item);
            uow.Save();

            TempData["message"] = "Дані про тип будівлі змінено";
            return RedirectToAction("BuildingType", "BuildingTypes", new { id = model.Id });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            repository.Delete(repository.Get(id));
            uow.Save();
            return RedirectToAction("Browse", "BuildingTypes");
        }

        private static IEnumerable<SelectListItem> SelectTypeNames(IBuildingTypeRepository repository)
        {
            return repository.GetAll().Select(e => new SelectListItem
            {
                Text = e.Name,
                Value = e.Name
            }).ToList();
        }

        private BuildingType CreateBuildingTypeObject(BuildingTypeEditingModel obj)
        {
            BuildingType entityObject = new BuildingType();
            entityObject.Id = obj.Id;
            entityObject.Name = obj.Name;
            entityObject.Parent = repository.GetByName(obj.ParentName);
            entityObject.ParentId = entityObject.Parent.Id;
            entityObject.Description = obj.Description;
            return entityObject;
        }
    }
}