using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Our_Ecommerce_store_GIS_40.Models.ViewModels
{
    public class Cat_Pro_Bra_viewModel
    {
        public List<Categories> Categories { get; set; }
        public List<Brands> Brands { get; set; }
        public List<Products> Products { get; set; }
        public Products product { get; set; }
        public Brands brand { get; set; }
        [Required(ErrorMessage ="Please Choose category for brand")]
        public int CatId { get; set; }
        public Categories category { get; set; }


    }
}