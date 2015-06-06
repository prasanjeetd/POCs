using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RNDApplications
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
             name: "PurchaseContact",
             url: "Purchase/GetContacts/{customerID}",
             defaults: new { controller = "Purchase", action = "GetContacts", customerID = UrlParameter.Optional }
         );

            //    routes.MapRoute(
            //    name: "PurchaseAddress",
            //    url: "Purchase/GetAddress/{customerID}",
            //    defaults: new { controller = "Purchase", action = "GetContacts", customerID = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Purchase", action = "Create", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "ProductKendo",
               url: "{controller}/{action}",
               defaults: new { controller = "ProductController", action = "ProductKendo" }
           );


        }
    }
}