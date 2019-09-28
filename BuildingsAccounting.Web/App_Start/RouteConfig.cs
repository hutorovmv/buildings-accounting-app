using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BuildingsAccounting.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "AllBuildings",
                url: "Будівлі",
                defaults: new
                {
                    controller = "Buildings",
                    action = "Browse"
                }
            );

            routes.MapRoute(
                name: "BuildingWithId",
                url: "Будівля_{id}",
                defaults: new
                {
                    controller = "Buildings",
                    action = "Building",
                },
                constraints: new
                {
                    id = @"\d+"
                }
            );

            routes.MapRoute(
                name: "BuildingsSelection",
                url: "Відбір_Будівель",
                defaults: new
                {
                    controller = "Buildings",
                    action = "Selection"
                }
            );

            routes.MapRoute(
                name: "AllBuildingTypes",
                url: "Типи_Будівель",
                defaults: new
                {
                    controller = "BuildingTypes",
                    action = "Browse"
                }
            );

            routes.MapRoute(
                name: "BuildingTypeWithId",
                url: "Тип_Будівлі_{id}",
                defaults: new
                {
                    controller = "BuildingTypes",
                    action = "BuildingType",
                },
                constraints: new
                {
                    id = @"\d+"
                }
            );

            routes.MapRoute(
                name: "BuildingTypesSelection",
                url: "Відбір_Типів_Будівель",
                defaults: new
                {
                    controller = "BuildingTypes",
                    action = "Selection"
                }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Buildings", action = "Browse", id = UrlParameter.Optional }
            );
        }
    }
}
