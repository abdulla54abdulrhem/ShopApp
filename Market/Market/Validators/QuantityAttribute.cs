using System.ComponentModel.DataAnnotations;
using Market.Models;
using Market.Data;
namespace Market.Validators
{
    public class QuantityAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            ApplicationDbContext _context = new ApplicationDbContext();
            CartItem cartItem = validationContext.ObjectInstance as CartItem;
            int q = cartItem.quantity;
            CartItem c = _context.CartItems.FirstOrDefault(x => x.Id == cartItem.Id);
            Product product = _context.products.FirstOrDefault(x => x.Id == c.ProductId);
            if (q <= product.quantity && q>=0)
                return ValidationResult.Success;
            return new ValidationResult("Quantity demanded is not valid");
        }

    }
}
