using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RNDApplications.Models;
using System.Drawing;
using System.IO;

namespace RNDApplications.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/
        RNDDBEntities db = new RNDDBEntities();

        public ActionResult ProductKendo()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddProductFromViewModel()
        {
            ProductViewModel productVM = new ProductViewModel();
            ProductMast product = new ProductMast();
            productVM.Product = product;
            productVM.productList = db.ProductMasts.ToList();
            productVM.Category = new SelectList(db.CategoryMasts, "CategoryID", "CategoryName");
            return View(productVM);
        }

        [HttpPost]
        public ActionResult AddProductFromViewModel(ProductViewModel productVM, IEnumerable<HttpPostedFileBase> productImage)
        {
            if (ModelState.IsValid)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    productImage.ToList()[0].InputStream.CopyTo(ms);
                    productVM.Product.ProductImage = ms.GetBuffer();
                }
                
                //productVM.Product.CategoryID = Convert.ToInt64(Request.Form["Category"]);
                //productVM.Product.CategoryID = Convert.ToInt64(productVM.Category.SelectedValue);
                db.ProductMasts.Add(productVM.Product);
                db.SaveChanges();

                File(productVM.Product.ProductImage, "image/jpg", "ProductImage");
                return RedirectToAction("AddProductFromViewModel");
            }
            return View(productVM);
        }

        public ActionResult AddProduct()
        {
            ViewBag.Category = new SelectList(db.CategoryMasts, "CategoryID", "CategoryName");
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(ProductMast product)
        {
            if (ModelState.IsValid)
            {
                db.ProductMasts.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        public FileContentResult GetPhoto(long productID)
        {
            var img = db.ProductMasts.Find(productID).ProductImage;
            return File(img, "img/jpg");
        }

        public ActionResult AddCategory()
        {
            return View();
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductMast newMovie)
        {

            if (ModelState.IsValid)
            {
                //db.AddToMovies(newMovie);
                //db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View(newMovie);
            }
        }
    }


}
