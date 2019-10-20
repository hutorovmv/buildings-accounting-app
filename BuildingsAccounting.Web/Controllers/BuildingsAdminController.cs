using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using BuildingsInfo.EF.DataContext;
using BuildingsInfo.EF.Models;
using BuildingsAccounting.Web.Models;
using BuildingsAccounting.Web.Infrastructure;
using BuildingsInfo.EF.Repositories.Interfaces;
using BuildingsAccounting.Web.Filters;

namespace BuildingsAccounting.Web.Controllers
{
    [ForAuthenticatedOnly]
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

            //model.Photos = 
            Building obj = CreateBuildingObject(model);           
            repository.Create(obj);
            uow.Save();

            TempData["message"] = "Дані про будівлю збережено";
            return RedirectToAction("Create");
        }

        public ActionResult Edit(int id)
        {
            Building obj = repository.Get(id);

            if (User.Identity.GetUserId() == obj.UserId)
            {
                var model = (BuildingEditingModel)obj;
                ViewBag.BuildingTypeName = SelectTypeNames(btRepository);
                TempData["Controller"] = "BuildingsAdmin";
                TempData["Action"] = "Edit";
                return View(model);
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult Edit(BuildingEditingModel model)
        {
            Building obj = CreateBuildingObject(model);
            Building item = repository.Get(obj.Id);

            if (User.Identity.GetUserId() == item.UserId)
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.BuildingTypeName = SelectTypeNames(btRepository);
                    return View(model);
                }

                ModifyBuilding(item, obj);             

                repository.Update(item);
                uow.Save();

                TempData["message"] = "Дані про будівлю змінено";
            }
            
            return RedirectToAction("Edit", "BuildingsAdmin", new { id = model.Id });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Building item = repository.Get(id);

            if (User.Identity.GetUserId() == item.UserId)
            {
                repository.Delete(item);
                uow.Save();
                DeleteFiles(item.Photos);

                return RedirectToAction("Browse", "Buildings");
            }

            return RedirectToAction("Login", "Account");
        }

        public ActionResult ShowOwn()
        {
            if (User.Identity.IsAuthenticated) {
                string id = User.Identity.GetUserId();
                IEnumerable<BuildingTableModel> model = repository.GetUserBuildings(id).Select(e => (BuildingTableModel)e).ToList();
                return View(model);
            }

            return RedirectToRoute(new { controller = "Account", action = "Login" });
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
            entityObject.UserId = User.Identity.GetUserId();
            //entityObject.UserId = obj.UserId;

            if (obj.Photos == null)
                entityObject.Photos = SaveFiles(obj.Files);
            else
                entityObject.Photos = obj.Files != null ? obj.Photos.Union(SaveFiles(obj.Files)).ToArray() : obj.Photos;

            return entityObject;
        }

        private static void ModifyBuilding(Building item, Building obj)
        {
            item.Address = obj.Address;
            item.BuildingTypeId = obj.BuildingTypeId;
            item.BuildingType = obj.BuildingType;
            item.FloorsNumber = obj.FloorsNumber;
            item.Area = obj.Area;
            item.Photos = obj.Photos;
            item.Note = obj.Note;
            item.Description = obj.Description;
            item.UserId = obj.UserId;
        }

        private string[] SaveFiles(IEnumerable<HttpPostedFileBase> files)
        {
            string path = (string)HttpContext.Application["ImagesPath"];
            List<string> fileNames = new List<string>();

            if (files != null)
            {
                foreach (var f in files)
                {
                    Guid id = Guid.NewGuid();
                    string ext = Path.GetExtension(f.FileName);
                    fileNames.Add(id + ext);

                    f.SaveAs(Server.MapPath(path + id + ext));
                }
            }

            return fileNames.ToArray();
        }

        private void DeleteFiles(string[] photos)
        {
            foreach (var f in photos)
            {
                string path = HttpContext.Server.MapPath((string)HttpContext.Application["ImagesPath"] + f);
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);
            }
        }
    }
}