using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuildingsInfo.EF.Models;
using BuildingsAccounting.Web.Infrastructure;

namespace BuildingsAccounting.Web.Controllers
{
    public class BuildingTypesController : Controller
    {
        private IEnumerable<BuildingType> objects;

        public IEnumerable<BuildingType> Objects
        {
            get { return objects; }
            set
            {
                Objects = value;
            }
        }

        public BuildingTypesController()
        {
            Objects = UowCreator.Uow.BuildingTypeRepository.GetAll();
        }
    }
}