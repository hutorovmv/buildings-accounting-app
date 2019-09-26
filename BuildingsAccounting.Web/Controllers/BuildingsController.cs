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
        private IEnumerable<Building> objects;
        private IEnumerable<BuildingTableModel> tableModelObjects;

        public IEnumerable<Building> Objects
        {
            get { return objects; }

            set
            {
                objects = value;
                tableModelObjects = objects.Select(e => (BuildingTableModel)e).OrderBy(e => e);
            }
        }

        public BuildingsController()
        {
            Objects = UowCreator.Uow.BuildingRepository.GetAll();
        }

        public ViewResult Browse()
        {
            return View(tableModelObjects);
        }
    }
}