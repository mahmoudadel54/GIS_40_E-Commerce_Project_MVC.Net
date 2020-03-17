using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Our_Ecommerce_store_GIS_40.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("Product")]
        public int FK_Product { get; set; }
        public virtual Products Product { get; set; }
        [ForeignKey("Order")]
        public int FK_order { get; set; }
        public virtual Order Order { get; set; }
    }
}