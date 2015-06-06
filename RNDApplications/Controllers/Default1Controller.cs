using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RNDApplications.Models;
using System.Data;

namespace RNDApplications.Controllers
{
    public class Default1Controller : Controller
    {
        private RNDDBEntities db = new RNDDBEntities();

        //
        // GET: /Default2/

        public ActionResult Index()
        {
            var productmasts = db.ProductMasts.Include(p => p.CategoryMast);
            return View(productmasts.ToList());
        }

        //
        // GET: /Default2/Details/5

        public ActionResult Details(long id = 0)
        {
            ProductMast productmast = db.ProductMasts.Find(id);
            if (productmast == null)
            {
                return HttpNotFound();
            }
            return View(productmast);
        }

        //
        // GET: /Default2/Create

        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.CategoryMasts, "CategoryID", "CategoryName");
            return View();
        }

        //
        // POST: /Default2/Create

        [HttpPost]
        public ActionResult Create(ProductMast productmast)
        {
            if (ModelState.IsValid)
            {
                db.ProductMasts.Add(productmast);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.CategoryMasts, "CategoryID", "CategoryName", productmast.CategoryID);
            return View(productmast);
        }

        //
        // GET: /Default2/Edit/5

        public ActionResult Edit(long id = 0)
        {
            ProductMast productmast = db.ProductMasts.Find(id);
            if (productmast == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.CategoryMasts, "CategoryID", "CategoryName", productmast.CategoryID);
            return View(productmast);
        }

        //
        // POST: /Default2/Edit/5

        [HttpPost]
        public ActionResult Edit(ProductMast productmast)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productmast).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.CategoryMasts, "CategoryID", "CategoryName", productmast.CategoryID);
            return View(productmast);
        }

        //
        // GET: /Default2/Delete/5

        public ActionResult Delete(long id = 0)
        {
            ProductMast productmast = db.ProductMasts.Find(id);
            if (productmast == null)
            {
                return HttpNotFound();
            }
            return View(productmast);
        }

        //
        // POST: /Default2/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            ProductMast productmast = db.ProductMasts.Find(id);
            db.ProductMasts.Remove(productmast);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
