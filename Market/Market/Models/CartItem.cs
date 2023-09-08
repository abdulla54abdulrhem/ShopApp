using System.ComponentModel.DataAnnotations;
using Market.Validators;
namespace Market.Models
{
    public class CartItem
    {
      
        public int Id { get; set; }
        public int ProductId { get; set; }
      
        public virtual Product Product { get; set; }
        [Quantity]
        public int quantity { get; set; }
        public int ?CartId { get; set; }
        public virtual Cart? Cart { get; set; }
        public float? Price { get; set; }
    }
}
