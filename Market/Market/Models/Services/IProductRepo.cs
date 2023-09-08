using Market.ViewModel;
namespace Market.Models.Services
{
    public interface IProductRepo
    {
        public List<Product> GetAll();
        public List<Product> GetFor(string Id);//for certain certain user
        //public Product Get(int Id);
        public void Insert(Product product);
        public void Update(Product product);
        public void Delete(Product product);
        public ProductDetails Get(int Id);
        public Product GetProduct(int Id);
        public void Save();
    }
}
