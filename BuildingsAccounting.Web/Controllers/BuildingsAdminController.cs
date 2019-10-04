using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuildingsInfo.EF.Models;
using BuildingsAccounting.Web.Models;
using BuildingsAccounting.Web.Infrastructure;
using BuildingsInfo.EF.Repositories.Interfaces;

namespace BuildingsAccounting.Web.Controllers
{
    public class BuildingsAdminController : Controller
    {
        private static IBuildingsInfoUOW uow = UowCreator.Uow;
        private IBuildingRepository repository = uow.BuildingRepository;
        private IBuildingTypeRepository btRepository = uow.BuildingTypeRepository;

        public ViewResult Create()
        {
            ViewBag.BuildingTypeName = SelectTypeNames(btRepository);
            TempData["Controller"] = "BuildingsAdmin";
            TempData["Action"] = "Create";
            return View(new BuildingEditingModel());
        }

        [HttpPost]
        public ActionResult Create(BuildingEditingModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.BuildingTypeName = SelectTypeNames(btRepository);
                return View(model);
            }

            SaveFiles(model.Files);
            repository.Create(CreateBuildingObject(model));
            uow.Save();

            TempData["message"] = "Дані про будівлю збережено";
            return RedirectToAction("Create");
        }

        public ViewResult Edit(int id)
        {
            var model = (BuildingEditingModel)repository.Get(id);
            ViewBag.BuildingTypeName = SelectTypeNames(btRepository);
            TempData["Controller"] = "BuildingsAdmin";
            TempData["Action"] = "Edit";
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(BuildingEditingModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.BuildingTypeName = SelectTypeNames(btRepository);
                return View(model);
            }

            SaveFiles(model.Files);
            Building obj = CreateBuildingObject(model);
            Building item = repository.Get(obj.Id);

            item.Address = obj.Address;
            item.BuildingTypeId = obj.BuildingTypeId;
            item.BuildingType = obj.BuildingType;
            item.FloorsNumber = obj.FloorsNumber;
            item.Area = obj.Area;
            item.Photos = obj.Photos;
            item.Note = obj.Note;
            item.Description = obj.Description;

            repository.Update(item);
            uow.Save();

            TempData["message"] = "Дані про будівлю змінено";
            //return RedirectToAction("Building", "Buildings", new { id = model.Id });
            return RedirectToAction("Edit", "BuildingsAdmin", new { id = model.Id });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Building item = repository.Get(id);

            repository.Delete(item);
            uow.Save();
            
            foreach (var f in item.Photos)
            {
                string path = Path.Combine((string)HttpContext.Application["ImagesPath"], f);
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);
            }

            return RedirectToAction("Browse", "Buildings");
        }

        private static IEnumerable<SelectListItem> SelectTypeNames(IBuildingTypeRepository repository)
        {
            return repository.GetAll().Select(e => new SelectListItem
            {
                Text = e.Name,
                Value = e.Name
            }).ToList();
        }

        private Building CreateBuildingObject(BuildingEditingModel obj)
        {
            Building entityObject = new Building();
            entityObject.Id = obj.Id;
            entityObject.Address = obj.Address;
            entityObject.BuildingType = btRepository.GetByName(obj.BuildingTypeName);
            entityObject.BuildingTypeId = entityObject.BuildingType.Id;
            entityObject.FloorsNumber = obj.FloorsNumber;
            entityObject.Area = obj.Area;
            entityObject.Note = obj.Note;
            entityObject.Description = obj.Description;

            if (obj.Photos == null)
                entityObject.Photos = obj.Files?.Select(e => e.FileName).ToArray();
            else
                entityObject.Photos = obj.Files != null ? obj.Photos.Union(obj.Files.Select(e => e.FileName)).ToArray() : obj.Photos;

            return entityObject;
        }

        private void SaveFiles(IEnumerable<HttpPostedFileBase> files)
        {
            if (files != null)
            {
                foreach (var f in files)
                {
                    f.SaveAs(Server.MapPath(this.HttpContext.Application["ImagesPath"] + f.FileName));
                }
            }
        }
    }
}