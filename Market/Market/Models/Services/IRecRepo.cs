using Market.Models;
namespace Market.Models.Services
{
    public interface IRecRepo
    {
        public void Add(int cartId);
        public List<Reciept> GetAll(string UserId);
        //we can add more functionalities as update, Delete in the future
        public void Save();
    }
}
