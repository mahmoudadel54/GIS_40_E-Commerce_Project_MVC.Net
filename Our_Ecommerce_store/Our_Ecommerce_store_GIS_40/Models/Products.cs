using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Our_Ecommerce_store_GIS_40.Models
{
    public class Products
    {
        public Products()
        {
            Approved = "No";
            orderDetail = new HashSet<OrderDetail>();
        }
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Name is Reuqires")]
        [MaxLength(50)]
        public string Name { get; set; }


        [Required(ErrorMessage = "Cost is Reuqires")]
        public int Cost { get; set; }
        /// <summary>
        /// Discount
        /// </summary>
        public int? Discount { get; set; }

        [Required(ErrorMessage = "Short Descripton is Reuqires")]
        [DisplayName("Short Description ")]
        public string Descripton { get; set; }


        [Required(ErrorMessage = "Long Descripton is Reuqires")]
        [DisplayName("Long Description")]
        public string Descripton2 { get; set; }


        [Required(ErrorMessage = "Uploading Photo is Reuqired")]
        public string Imageurl { get; set; }


        [Required(ErrorMessage = "Please Choose the Category of this product")]
        [DisplayName("Category")]
        [ForeignKey("cat")]
        public int cat_Id { get; set; }
        public virtual Categories cat { get; set; }

        [DisplayName("Status")]
        public string Approved { get; set; }


        [DisplayName("Brand")]
        [ForeignKey("brand")]
        public int? brand_Id { get; set; }
        public virtual Brands brand { get; set; }

        //public virtual string SecondBrand { get; set; }
        public virtual string owner_Id { get; set; }

        public virtual List<ApplicationUser> User { get; set; }

        public virtual ICollection<OrderDetail> orderDetail { get; set; }
        //public int Id { get; set; }
        //[Required]
        //[MaxLength(50)]
        //public string Name { get; set; }
        //public int Cost { get; set; }
        //public string Short_Descrip { get; set; }
        //public string Full_Descrip { get; set; }
        //public string Imageurl { get; set; }

        //[ForeignKey("category")]
        //public int category_Id { get; set; }
        //public virtual Categories category { get; set; }

        //[ForeignKey("brand")]
        //public int brand_Id { get; set; }
        //public virtual Brands brand { get; set; }
    }
}