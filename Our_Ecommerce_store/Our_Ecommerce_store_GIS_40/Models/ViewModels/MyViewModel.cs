using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Our_Ecommerce_store_GIS_40.Models.ViewModels
{
    public class MyViewModel
    {
        [Required]
        public string CategoryId { get; set; }

        public IEnumerable<Categories> Categories { get; set; }
    }
}