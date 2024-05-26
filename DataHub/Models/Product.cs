using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataHub.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Display(Name = "Product Image")]
        [Required(ErrorMessage = "Image is required.")]
        public string? ProductImage { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required.")]
        public string ProductName { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required.")]
        public string ProductDescription { get; set; }

        [Display(Name = "Price")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        [Required(ErrorMessage = "Price is required.")]

        public double ProductPrice { get; set; }

        [Display(Name = "Stock")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock can't be a negative number.")]
        [Required(ErrorMessage = "Stock is required.")]
        public int ProductStock { get; set; }

        public string? ImgURL(string account, string container)
        {
            string result = "https://" + account + ".blob.core.windows.net/" + container + "/" + this.ProductImage;
            return result;
        }


        //Relationships
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }


    }
}
