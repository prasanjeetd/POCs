using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RNDApplications.Models.New_Models;
using System.Globalization;
using System.Data;
//using System.Data.Objects;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.Reporting.WebForms;

namespace RNDApplications.Controllers
{
    public class PurchaseController : Controller
    {
        //
        // GET: /Purchase/

        PurchaseEntities db = new PurchaseEntities();

        public ActionResult Index(int id)
        {
            return View();
        }

        public ActionResult GetContacts1(long customerID)
        {
            Response.Write(db.CustomerMasts.Find(customerID).Contact);
            return null;
        }

        public string GetContacts(long customerID)
        {
            System.Threading.Thread.Sleep(500);
            return db.CustomerMasts.Find(customerID).Contact;
        }

        public FileContentResult GetProductImage(long productID)
        {
            var product = db.ProductMasts.Find(productID).ProductImage;
            if (product == null)
                return null;
            else
                return File(product, "img/jpg");
        }

        public JsonResult GetProductRate(long productID)
        {
            ProductMast product = db.ProductMasts.Find(productID);
            string img = product.ProductImage == null ? "" : Convert.ToBase64String(product.ProductImage.ToArray());
            return Json(new { Rate = product.Rate, ProductImage = img }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAddress(long customerID)
        {
            System.Threading.Thread.Sleep(500);
            return Json(new { Address = db.CustomerMasts.Find(customerID).Address }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetJsonData()
        {
            var persons = new List<Person>
                              {
                                  new Person{Id = 1, FirstName = "F1", 
                                      LastName = "L1", 
                                      Addresses = new List<Address>
                                                      {
                                                          new Address{Line1 = "LaneA"},
                                                          new Address{Line1 = "LaneB"}
                                                      }},
                                                                                                        
                                  new Person{Id = 2, FirstName = "F2", 
                                      LastName = "L2", 
                                      Addresses = new List<Address>
                                                      {
                                                          new Address{Line1 = "LaneC"},
                                                          new Address{Line1 = "LaneD"}
                                                      }}};

            return Json(persons, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            PurchaseViewModel purchaseVM = new PurchaseViewModel();

            purchaseVM.purchase = db.PurchaseMasters.Find(23);// new PurchaseMaster();
            purchaseVM.purchase.PurchaseDate = DateTime.Now.Date;
            purchaseVM.purchaseDetailList = db.PurchaseDetails.Where(d => d.PurchaseID == 23).ToList();//

            //TempData["PurchaseDetails"] = purchaseVM.purchaseDetailList;
            purchaseVM.customers = new SelectList(db.CustomerMasts.OrderBy(c => c.FullName), "CustomerID", "FullName");
            purchaseVM.products = new SelectList(db.ProductMasts.OrderBy(p => p.ProductName), "ProductID", "ProductName");
            return View(purchaseVM);
        }

        public ActionResult Delete()
        {
            return RedirectToAction("Create");
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "PurchaseViewModel,purchase,purchaseDetail,purchaseDetailList")]  PurchaseViewModel purchaseVM, string Command, int? delete)
        {

            List<PurchaseDetail> purchaseDetailsList = null;

            #region Using TempData
            //if (TempData["PurchaseDetails"] != null)
            //{
            //    TempData.Keep("PurchaseDetails");
            //    purchaseDetailsList = (List<PurchaseDetail>)TempData["PurchaseDetails"];
            //}
            //else
            //    purchaseDetailsList = new List<PurchaseDetail>();
            #endregion

            if (purchaseVM.purchaseDetailList != null && purchaseVM.purchaseDetailList.Count > 0)
            {
                purchaseVM.purchaseDetailList.ForEach(x => x.Total = x.Rate * x.Qty);
                purchaseDetailsList = purchaseVM.purchaseDetailList;
                //purchaseDetailsList.Reverse();
            }


            if (ModelState.IsValid)
            {
                if (Command == "Submit")
                {
                    PurchaseMaster purchase = purchaseVM.purchase;

                    #region Using Purchase ViewModel
                    //foreach (PurchaseDetail pvm in purchaseDetailsList)
                    //{
                    //    purchase.PurchaseDetails.Add(new PurchaseDetail
                    //    {
                    //        ProductID = pvm.ProductID,
                    //        Rate = pvm.Rate,
                    //        Qty = pvm.Qty,
                    //        Total = pvm.Total
                    //    });
                    //}
                    #endregion

                    if (purchase.PurchaseID == 0)
                    {
                        purchaseDetailsList.ForEach(d => purchase.PurchaseDetails.Add(d));
                        purchase.PurchaseDate = DateTime.Now.Date;// purchase.PurchaseDate;
                        db.PurchaseMasters.Add(purchase);
                        db.SaveChanges();
                        //DateTime dt = DateTime.ParseExact(purchase.PurchaseDate.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        purchase.InvoiceNo = purchase.PurchaseDate.Value.ToString("dd/MM/yyyy").Replace("-", "") + purchase.PurchaseID.ToString();
                        db.SaveChanges();
                    }
                    else
                    {
                        PurchaseMaster originalPurchase = db.PurchaseMasters.Find(purchase.PurchaseID);
                        originalPurchase.CustomerID = purchase.CustomerID;
                        originalPurchase.Contact = purchase.Contact;
                        originalPurchase.Address = purchase.Address;
                        purchase = originalPurchase;

                        #region Using List Foreach
                        //purchaseDetailsList.ForEach(d =>
                        //    {
                        //        if (d.PurchaseDetailID == 0)
                        //            originalPurchase.PurchaseDetails.Add(d);
                        //        else
                        //            db.Entry(d).State = EntityState.Modified; 

                        //    }
                        //    );
                        #endregion

                        foreach (PurchaseDetail purchaseDet in purchaseDetailsList)
                        {
                            if (purchaseDet.PurchaseDetailID == 0)
                                purchase.PurchaseDetails.Add(purchaseDet);
                            else
                            {
                                PurchaseDetail pd = db.PurchaseDetails.Find(purchaseDet.PurchaseDetailID);
                                pd.Qty = purchaseDet.Qty;
                                pd.Rate = purchaseDet.Rate;
                                pd.Total = purchaseDet.Qty * purchaseDet.Rate;
                                pd.ProductID = purchaseDet.ProductID;
                                db.Entry(pd).State = EntityState.Modified;
                            }
                        }

                        if (TempData["deletePDIDList"] != null)
                            ((List<long>)TempData["deletePDIDList"]).ForEach(d => db.PurchaseDetails.Remove(db.PurchaseDetails.Find(d)));

                        db.SaveChanges();
                        TempData.Remove("deletePDIDList");
                    }
                    purchaseVM.purchase = purchase;
                }
                else if (Command == "Add")
                {
                    PurchaseDetail purchaseDetail = purchaseVM.purchaseDetail;
                    purchaseDetail.Total = purchaseDetail.Rate * purchaseDetail.Qty;

                    #region Using Purchase ViewModel
                    //purchaseDetailVM.ProductID = purchaseDetail.ProductID;
                    //purchaseDetailVM.ProductName = db.ProductMasts.Find(purchaseDetail.ProductID).ProductName;
                    //purchaseDetailVM.Rate = purchaseDetail.Rate;
                    //purchaseDetailVM.Qty = purchaseDetail.Qty;
                    //purchaseDetailVM.Total = purchaseDetail.Rate * purchaseDetail.Qty;
                    #endregion

                    if (purchaseDetailsList == null)
                        purchaseDetailsList = new List<PurchaseDetail>();
                    purchaseDetailsList.Insert(0, purchaseDetail);
                }
                else if (delete != null)
                {
                    int deleteID = (int)delete;
                    List<long> deletePDIDList;
                    long pdID = purchaseDetailsList[deleteID].PurchaseDetailID;
                    if (pdID != 0)
                    {
                        if (TempData["deletePDIDList"] == null)
                            deletePDIDList = new List<long>();
                        else
                        {
                            TempData.Keep("deletePDIDList");
                            deletePDIDList = (List<long>)TempData["deletePDIDList"];
                        }
                        deletePDIDList.Add(pdID);
                        TempData["deletePDIDList"] = deletePDIDList;
                    }
                    purchaseDetailsList.RemoveAt(deleteID);
                }
            }

            ModelState.Clear();

            purchaseVM.purchaseDetailList = purchaseDetailsList;
            purchaseVM.customers = new SelectList(db.CustomerMasts.OrderBy(c => c.FullName), "CustomerID", "FullName", purchaseVM.purchase.CustomerID);
            purchaseVM.products = new SelectList(db.ProductMasts.OrderBy(p => p.ProductName), "ProductID", "ProductName");
            //TempData["PurchaseDetails"] = purchaseDetailsList;
            purchaseVM.purchaseDetail = null;
            return View(purchaseVM);

        }

        public ActionResult Edit(long purchaseID = 2)
        {
            PurchaseViewModel purchaseVM = new PurchaseViewModel();
            purchaseVM.purchase = db.PurchaseMasters.Find(2);
            purchaseVM.purchase.PurchaseDate = DateTime.Now.Date;
            purchaseVM.purchaseDetailList = db.PurchaseDetails.Where(d => d.PurchaseID == 2).ToList();
            purchaseVM.customers = new SelectList(db.CustomerMasts.OrderBy(c => c.FullName), "CustomerID", "FullName");
            purchaseVM.products = new SelectList(db.ProductMasts.OrderBy(p => p.ProductName), "ProductID", "ProductName");
            return View(db.PurchaseMasters.Find(purchaseID));
        }

        [HttpPost]
        public ActionResult Edit(PurchaseMaster purchaseVM)
        {
            return View(purchaseVM);
        }

        public ActionResult ShowReport(long purchaseID)
        {
            PurchaseMaster purchase = db.PurchaseMasters.Find(purchaseID);
            return View(purchase);
        }

        public FileContentResult DisplayReport(long purchaseID, string format = "jpeg")
        {
            LocalReport report = new LocalReport();
            report.ReportPath = Server.MapPath("~/Reports/PurchaseReport.rdlc");
            ReportDataSource reportDataSource = new ReportDataSource("PurchaseDS");
            reportDataSource.Value = (from p in db.PurchaseMasters
                                      join d in db.PurchaseDetails
                                      on p.PurchaseID equals d.PurchaseID
                                      join p1 in db.ProductMasts
                                      on d.ProductID equals p1.ProductID
                                      join c in db.CustomerMasts
                                      on p.CustomerID equals c.CustomerID
                                      where p.PurchaseID == purchaseID
                                      select new
                                      {
                                          p.InvoiceNo,
                                          p.PurchaseDate,
                                          p1.ProductName,
                                          c.FullName,
                                          p.Address,
                                          p.Contact,
                                          d.Rate,
                                          d.Qty,
                                          d.Total
                                      }).ToList();
            report.DataSources.Add(reportDataSource);
            string reportType = "";
            if (format == "jpeg")
                reportType = "Image";
            else
                reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension;
            //The DeviceInfo settings should be changed based on the reportType            
            //http://msdn2.microsoft.com/en-us/library/ms155397.aspx            
            string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>jpeg</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>1in</MarginLeft>" +
                "  <MarginRight>1in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";
            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;
            //Render the report            
            renderedBytes = report.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            if (format == "jpeg")
                return File(renderedBytes, "image/jpeg");
            else
                return File(renderedBytes, "pdf","Test.pdf");
        }

        public FileContentResult DisplayReportWithRV(long purchaseID)
        {
            var viewer = new ReportViewer();
            LocalReport report = new LocalReport();
            viewer.LocalReport.ReportPath = Server.MapPath("~/Reports/PurchaseReport.rdlc");
            ReportDataSource reportDataSource = new ReportDataSource("PurchaseDS");
            reportDataSource.Value = (from p in db.PurchaseMasters
                                      join d in db.PurchaseDetails
                                      on p.PurchaseID equals d.PurchaseID
                                      join p1 in db.ProductMasts
                                      on d.ProductID equals p1.ProductID
                                      join c in db.CustomerMasts
                                      on p.CustomerID equals c.CustomerID
                                      where p.PurchaseID == purchaseID
                                      select new
                                      {
                                          p.InvoiceNo,
                                          p.PurchaseDate,
                                          c.FullName,
                                          p.Address,
                                          p.Contact,
                                          d.Rate,
                                          d.Qty,
                                          d.Total
                                      }).ToList();
            viewer.LocalReport.DataSources.Add(reportDataSource);
            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension;
            //The DeviceInfo settings should be changed based on the reportType            
            //http://msdn2.microsoft.com/en-us/library/ms155397.aspx            
            string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>image/jpeg</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>1in</MarginLeft>" +
                "  <MarginRight>1in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";
            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;
            //Render the report           
            viewer.LocalReport.Refresh();
            renderedBytes = viewer.LocalReport.Render(reportType, null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            return new FileContentResult(renderedBytes, mimeType);
            //return File(renderedBytes, "pdf"); 
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
