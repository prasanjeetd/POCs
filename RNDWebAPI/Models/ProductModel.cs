using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RNDWebAPI.Models
{
    public class ProductModel
    {

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal MRP { get; set; }

        public DateTime ExpireyDate { get; set; }

    }
}