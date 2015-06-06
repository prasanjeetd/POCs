using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RNDApplications.Models
{
    public class ProductViewModel
    {
        public ProductMast Product { get; set; }
        public SelectList Category { get; set; }

        public List<ProductMast> productList { get; set; }
    }
}