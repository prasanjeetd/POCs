using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RNDApplications.MyServiceReference;

namespace RNDApplications.Controllers
{
    public class MyController : Controller
    {
        //
        // GET: /My/

        public ActionResult Index()
        {
            MyServiceClient client = new MyServiceClient();
            MyClass myClass = new MyClass();
            myClass.Name = "Prasanjeet420";
            myClass= client.MyFunction2(myClass);
            return View();
        }

    }
}
