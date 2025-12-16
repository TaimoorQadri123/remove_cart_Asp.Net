using System.ComponentModel.DataAnnotations.Schema;

namespace ModelHandling.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Products Product { get; set; }  
        public int Quantity { get; set; }
    }
}
 