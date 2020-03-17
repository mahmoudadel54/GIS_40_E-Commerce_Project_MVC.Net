using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Our_Ecommerce_store_GIS_40.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerMail { get; set; }
        public string CustomerAddress { get; set; }
        public DateTime date { get; set; }
        public Order()
        {
            OrderDetail = new HashSet<OrderDetail>();
        }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}