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
        private IEnumerable<BuildingType> objects;
        private IEnumerable<BuildingTypeTableModel> tableModelObjects; 

        public IEnumerable<BuildingType> Objects
        {
            get { return objects; }
            set
            {
                objects = value;
                tableModelObjects = objects.Select(e => (BuildingTypeTableModel)e).OrderBy(e => e.Name);
            }
        }

        public BuildingTypesController()
        {
            Objects = UowCreator.Uow.BuildingTypeRepository.GetAll();
        }

        public ViewResult Browse()
        {
            return View(tableModelObjects);
        }
    }
}