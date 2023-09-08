using Market.Models;
using Market.Data;
using Microsoft.EntityFrameworkCore;

namespace Market.Models.Services
{
    public class RecRepo : IRecRepo
    {
        private ApplicationDbContext _context;
        //private ICartRepo _cartRepo;
        public RecRepo()
        {
            _context=new ApplicationDbContext();
            //_cartRepo = new CartRepo(); 
        }
        public void Add(int cartId)
        {
            Cart cart = _context.Carts
                .Include(c=>c.CartItems)
                .FirstOrDefault(x => x.Id == cartId);
            if (cart == null) return;
            Reciept reciept = new Reciept();
            reciept.UserId = cart.UserId;
            reciept.Total = 0;
            reciept.cartItems = new List<CartItem>();
            var cartItemsCopy = cart.CartItems.ToList();
            foreach (var item in cartItemsCopy)
            {
                Console.WriteLine(item.Id);
                reciept.Total += (float)(item.quantity * item.Price);
                cart.CartItems.Remove(item);
                item.CartId = null;
                reciept.cartItems.Add(item);
            }
            _context.Reciepts.Add(reciept);
        }

        public List<Reciept> GetAll(string UserId)
        {
            return _context.Reciepts
                .Include(c=>c.cartItems)
                .ThenInclude(ci=>ci.Product)
                .Where(x => x.UserId == UserId).ToList();
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
