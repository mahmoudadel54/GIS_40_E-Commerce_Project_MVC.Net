using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Our_Ecommerce_store_GIS_40.Models
{
    public class cart
    {
        public Products Product { get; set; }
        public int Quantity { get; set; }

        public cart(Products product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
    }
}