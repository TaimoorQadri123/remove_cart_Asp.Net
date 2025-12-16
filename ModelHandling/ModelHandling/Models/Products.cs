using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelHandling.Models
{
    public class Products
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public string ProductCatDescription { get; set; }

        public double Price { get; set; }
       
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]

     
        public Category? Category { get; set; }



            


    }
}
