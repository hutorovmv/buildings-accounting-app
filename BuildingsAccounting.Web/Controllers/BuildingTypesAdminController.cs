using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using BuildingsAccounting.Web.Models;
using BuildingsAccounting.Web.Infrastructure;
using BuildingsInfo.EF.DataContext;
using BuildingsInfo.EF.Models;
using BuildingsInfo.EF.Repositories.Interfaces;
using BuildingsAccounting.Web.Filters;
using Microsoft.AspNet.Identity.Owin;

namespace BuildingsAccounting.Web.Controllers
{
    [ForAuthenticatedOnly]
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

            BuildingType obj = CreateBuildingTypeObject(model);
            repository.Create(obj);
            uow.Save();

            TempData["message"] = "Дані про тип будівлі збережено";
            return RedirectToAction("Create");
        }

        public ActionResult Edit(int id)
        {
            BuildingType obj = repository.Get(id);

            if (User.Identity.GetUserId() == obj.UserId)
            {
                var model = (BuildingTypeEditingModel)obj;
                ViewBag.ParentName = SelectTypeNames(repository);
                return View(model);
            }

            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult Edit(BuildingTypeEditingModel model)
        {
            BuildingType obj = CreateBuildingTypeObject(model);
            BuildingType item = repository.Get(obj.Id);

            if (User.Identity.GetUserId() == item.UserId)
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.ParentName = SelectTypeNames(repository);
                    return View(model);
                }

                ModifyBuildingType(item, obj);
                repository.Update(item);
                uow.Save();

                TempData["message"] = "Дані про тип будівлі змінено";
            }

            return RedirectToAction("BuildingType", "BuildingTypes", new { id = model.Id });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            BuildingType item = repository.Get(id);

            if (User.Identity.GetUserId() == item.UserId)
            {
                repository.Delete(item);
                uow.Save();
                return RedirectToAction("Browse", "BuildingTypes");
            }

            return RedirectToAction("Login", "Account");
        }

        public ViewResult ShowOwn()
        {
            IEnumerable<BuildingTypeTableModel> model = HttpContext.GetOwinContext()
                .Get<ApplicationContext>().BuildingTypes
                .ToList().Select(e => (BuildingTypeTableModel)e);
            return View(model);
        }

        private static IEnumerable<SelectListItem> SelectTypeNames(IBuildingTypeRepository repository)
        {
            List<string> typeNames = new List<string>();
            typeNames.Add("---");
            typeNames.AddRange(repository.GetAll().Select(e => e.Name));
            
            return typeNames.Select(e => new SelectListItem { Text = e, Value = e }).ToList();
        }

        private BuildingType CreateBuildingTypeObject(BuildingTypeEditingModel obj)
        {
            BuildingType entityObject = new BuildingType();
            entityObject.Id = obj.Id;
            entityObject.Name = obj.Name;
            entityObject.Parent = repository.GetByName(obj.ParentName);
            entityObject.ParentId = entityObject.Parent?.Id;
            entityObject.Description = obj.Description;
            entityObject.UserId = User.Identity.GetUserId();
            //entityObject.UserId = obj.UserId;
            return entityObject;
        }

        private static void ModifyBuildingType(BuildingType item, BuildingType obj)
        {
            item.Name = obj.Name;
            item.ParentId = obj.ParentId;
            item.Parent = obj.Parent;
            item.Description = obj.Description;
            item.UserId = obj.UserId;
            //item.UserId = User.Identity.GetUserId();
        }
    }
}