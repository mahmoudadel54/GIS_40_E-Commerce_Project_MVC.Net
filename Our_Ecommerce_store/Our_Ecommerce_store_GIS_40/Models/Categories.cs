using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Our_Ecommerce_store_GIS_40.Models
{
    public class Categories
    {
        [Key]
        [Required(ErrorMessage = "You must choose Category for the brand")]
        public int Id { get; set; }
        public Categories()
        {
            Brands = new HashSet<Brands>();
            Products = new HashSet<Products>();
        }
        [Required]
        [MaxLength(50)]
        [DisplayName("Category Type")]
        public string Name { get; set; }

        public virtual ICollection<Brands> Brands { get; set; }

        public virtual ICollection<Products> Products { get; set; }

    }
}