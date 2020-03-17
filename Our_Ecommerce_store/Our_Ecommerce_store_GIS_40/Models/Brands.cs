using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Our_Ecommerce_store_GIS_40.Models
{
    public class Brands
    {
        public Brands()
        {
            cat_List = new HashSet<Categories>();
            products = new HashSet<Products>();
            approved = "No";

        }
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage ="You must enter name of the brand")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Please choose Category")]
        public virtual ICollection<Categories> cat_List { get; set; }

        public virtual ICollection<Products> products { get; set; }
        public string approved { get; set; }
    }
}