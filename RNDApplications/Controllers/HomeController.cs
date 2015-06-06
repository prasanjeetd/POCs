using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RNDApplications.ServiceReference1;
using System.Net;
using System.Text;

namespace RNDApplications.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
        public ActionResult Pages(string pageName)
        {
            string download = "";
            using (WebClient client1 = new WebClient())
            {
                //client.Headers["User-Agent"] =
                //"Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) " +
                //"(compatible; MSIE 6.0; Windows NT 5.1; " +
                //".NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                client1.Headers.Add("content-type", "application/json");
                // Download data.
                byte[] arr = client1.DownloadData("http://localhost:4658/api/Product");

                download = Encoding.ASCII.GetString(arr);
                // Write values.
                //Console.WriteLine("--- WebClient result ---");
                //Console.WriteLine(arr.Length);
            }

            Service1Client client = new Service1Client();
            CompositeType comT = new CompositeType();

            if (pageName != null)
            {
                comT.BoolValue = true;
                comT.StringValue = pageName;
            }

            CompositeType ct = client.GetDataUsingDataContract(comT);
            ViewBag.PageTitle = ct.StringValue;

            return View(ct);

            //string tp = client.GetData(4);
        }
    }
}
