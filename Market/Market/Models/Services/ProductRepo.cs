using Market.ViewModel;
using Market.Data;
namespace Market.Models.Services
{
    public class ProductRepo : IProductRepo
    {
        private ApplicationDbContext _context;
        public ProductRepo()
        {
            _context = new ApplicationDbContext();
        }
        public void Delete(Product product)
        {
            _context.products.Remove(product);
        }

        public ProductDetails Get(int Id)
        {
            Product product = _context.products.FirstOrDefault(x => x.Id == Id);
            string sellerName = _context.AppUsers.FirstOrDefault(x => x.Id == product.SellerId).UserName;
            ProductDetails details = new ProductDetails();
            details.product = product;
            details.SellerName = sellerName;
            return details;
        }

        public List<Product> GetAll()
        {
            return _context.products.ToList();
        }

        public List<Product> GetFor(string Id)
        {
            return _context.products.Where(x => x.SellerId == Id).ToList();
        }

        public void Insert(Product product)
        {
            _context.products.Add(product);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Product product)
        {
            Product product1 = _context.products.FirstOrDefault(x => x.Id == product.Id);
            product1.Name = product.Name;
            product1.quantity = product.quantity;
            product1.Price = product.Price;
            product1.Description = product.Description;
            product1.PictureUrl = product.PictureUrl;
        }
        public Product GetProduct(int Id)
        {
            return _context.products.FirstOrDefault(x => x.Id == Id);
        }
    }
}
