using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public virtual List<CartItem> ?CartItems { get; set; }
        [ForeignKey("AppUser")]
        public string UserId { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
