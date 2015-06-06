using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RNDApplications.Models.New_Models
{
    public class PurchaseViewModel
    {

        public PurchaseMaster purchase { get; set; }

        public PurchaseDetail purchaseDetail { get; set; }

        //public List<PurchaseDetailViewModel> purchaseDetailVMList { get; set; }

        public List<PurchaseDetail> purchaseDetailList { get; set; }
       
        public SelectList customers { get; set; }

        public SelectList products { get; set; }
    }

    public class PurchaseDetailViewModel
    {
        private long? productID;
        public long? ProductID {
            get { return productID; }
            set {productID=value ;} 
        }

        public string ProductName { get; set; }

        public decimal? Rate { get; set; }

        public int? Qty { get; set; }

        public decimal? Total { get; set; }


    }

    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<Address> Addresses { get; set; }
    }

    public class Address
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}