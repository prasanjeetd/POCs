using RNDWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace RNDWebAPI.Controllers
{
    public class ProductController : ApiController
    {
        
        public ProductController()
        {
            
        }

        #region  "STATIC DATA "

        private List<ProductModel> FillProducts()
        {
            List<ProductModel> products = new List<ProductModel>();

            ProductModel product;
            for (int i = 1; i <= 2; i++)
            {
                product = new ProductModel();
                product.ProductId = i;
                product.ProductName = "Product " + i;
                product.MRP = 1000 * i;
                product.ExpireyDate = DateTime.Now.AddDays(100 + i);
                products.Add(product);
            }

            return products;
        }

        public List<ProductModel> Get()
        {
            return FillProducts();
        }

        public ProductModel Get(int id)
        {
            List<ProductModel> products = FillProducts();

            ProductModel product = products.Find(x => x.ProductId == id);

            return product;
        }

        public ProductModel Post(ProductModel product)
        {
            product.ProductId = FillProducts().Count + 1;

            return product;
        }

        #endregion

        //public HttpResponseMessage Get()
        //{
        //    return Request.CreateResponse(HttpStatusCode.OK,shopContext.ProductMasts.ToList());
        //}
    }
}
