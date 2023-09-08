using Market.Models;
using Market.ViewModel;
using Market.Data;
using Microsoft.EntityFrameworkCore;

namespace Market.Models.Services
{
    public class CartRepo : ICartRepo
    {
        private ApplicationDbContext _context;
        public CartRepo()
        {
            _context = new ApplicationDbContext();
        }
        public void Add(ProductDetails productDetails,string UserId)
        {
            CartItem item = new CartItem();
            item.ProductId = productDetails.product.Id;
            item.quantity = productDetails.product.quantity;
            Product p = _context.products.FirstOrDefault(x => x.Id == productDetails.product.Id);
            p.quantity -= item.quantity;
            item.Price = p.Price;
            //AppUser appUser = _context.AppUsers.FirstOrDefault(x => x.Id == UserId);
            Cart cart = _context.Carts.FirstOrDefault(x => x.UserId==UserId);
            if (cart.CartItems == null)
            {
                cart.CartItems = new List<CartItem>();
            }
            cart.CartItems.Add(item);
            //_context.CartItems.Add(item);
        }

        public void Delete(int Id)
        {
            CartItem cartItem = _context.CartItems
                .Include(c=>c.Product)
                .Include(c=>c.Cart)
                .FirstOrDefault(x=>x.Id == Id);
           
            cartItem.Product.quantity+=cartItem.quantity;
            //AppUser appUser = _context.AppUsers.FirstOrDefault(x => x.Id == UserId);
            Cart cart = _context.Carts.FirstOrDefault(x => x.Id == cartItem.CartId);
            cart.CartItems.Remove(cartItem);
            _context.CartItems.Remove(cartItem);
        }
        public void DeleteFromCart(int Id)
        {
            CartItem cartItem = _context.CartItems
                 .Include(c => c.Cart)
                 .FirstOrDefault(x => x.Id == Id);
            //AppUser appUser = _context.AppUsers.FirstOrDefault(x => x.Id == UserId);
            Cart cart = _context.Carts.FirstOrDefault(x => x.Id == cartItem.CartId);
            
            cart.CartItems.Remove(cartItem);
            cartItem.CartId = null;
            _context.CartItems.Remove(cartItem);
        }
        public List<CartItem>? GetAll(string UserId)
        {
            //AppUser appUser = _context.AppUsers.FirstOrDefault(x => x.Id == UserId);
            Cart cart = _context.Carts
             .Include(c => c.CartItems)
             .ThenInclude(ci=>ci.Product)
            .FirstOrDefault(x => x.UserId == UserId);
            return cart.CartItems.ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(CartItem c)
        {

            CartItem oldItem = _context.CartItems
            .Include(c => c.Product)
            .FirstOrDefault(x => x.Id == c.Id);
            //this when user go to invalid url it do nothing
            if (oldItem == null) return;
            //this if the user somehow added more quantity than the bound, then it do nothing
           
                
            int DiffQuantity = c.quantity - oldItem.quantity;
                
            oldItem.quantity += DiffQuantity;
                oldItem.Product.quantity -= DiffQuantity;

        }
        public int GetCartId(string UserId)
        {
            return _context.Carts.FirstOrDefault(c=>c.UserId==UserId).Id;
        }
      
        public CartItem Get(int Id)
        {
            return _context.CartItems
                .Include(c=>c.Product)
                .FirstOrDefault(x => x.Id == Id);
        }
    }
}
