using Microsoft.AspNetCore.Mvc;
using Market.Models.Services;
using Microsoft.AspNetCore.Identity;
using Market.Models;
using Microsoft.AspNetCore.Authorization;

namespace Market.Controllers
{
    public class ProductController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private IProductRepo productRepo = new ProductRepo();
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProductController(UserManager<AppUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;
        }
        public async Task<string> GetUserId()
        {
            var user = await _userManager.GetUserAsync(User);
            return user.Id;
        }
        [Authorize(Roles = "User")]
        public IActionResult Index()
        {
            return View(productRepo.GetAll());
        }
        [Authorize(Roles ="Seller")]
        public async  Task<IActionResult> Add(Product p)
        {
            // i want to pass the seller Id to the View
            var user = await _userManager.GetUserAsync(User);
            ViewBag.SellerId = user.Id;
            return View();
        }
        public async Task AddPhoto(Product p)
        {
            
            if (p.Picture != null)
            {
                string folder = "Images";
                string fileName = Guid.NewGuid().ToString() + '_' + Path.GetExtension(p.Picture.FileName);
                string serverFolder = Path.Combine(webHostEnvironment.WebRootPath, folder, fileName);
                await p.Picture.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                p.PictureUrl = "/Images/" + fileName;
            }
            
        }
        [Authorize(Roles ="Seller")]
        public async Task<IActionResult> SaveAdd(Product p)
        {

            //if (p.Picture != null)
            //{
            //    string folder = "Images";
            //    string fileName = Guid.NewGuid().ToString() + '_' + Path.GetExtension(p.Picture.FileName);
            //    string serverFolder = Path.Combine(webHostEnvironment.WebRootPath, folder, fileName);
            //    await p.Picture.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            //    p.PictureUrl = "/Images/"+fileName;
            //}
            if (ModelState.IsValid)
            {
                await AddPhoto(p);
                productRepo.Insert(p);
                productRepo.Save();
                return RedirectToAction("MyProducts");
            }
            return View("Add", p);
        }
        public IActionResult Details(int Id)
        {
            return View(productRepo.Get(Id));
        }
        [Authorize(Roles ="Seller")]
        
        public IActionResult Edit(int Id)
        {
            ViewBag.Id = Id;
            return View(productRepo.GetProduct(Id));
        }
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> SaveEdit(Product p)
        {
            if (ModelState.IsValid)
            {
                if (p.Picture != null)
                {
                    await AddPhoto(p);
                }
                productRepo.Update(p);
                productRepo.Save();
                return RedirectToAction("MyProducts");
            }
            return View("Edit", p);
        }
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> MyProducts()
        {
            return View(productRepo.GetFor(await GetUserId()));
        }
    }
}
