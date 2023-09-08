using Market.Models;
using Market.ViewModel;
namespace Market.Models.Services
{
    public interface ICartRepo
    {
        // I have to pass the UserId to get the cart from the database
        public List<CartItem> GetAll(string UserId);
        public void Add(ProductDetails productDetails,string UserId);
        public CartItem Get(int Id);
        public void Update(CartItem c);
        public void Delete(int Id);
        public void Save();
        public int GetCartId(string UserId);
        public void DeleteFromCart(int Id);
    }
}
