using Microsoft.AspNetCore.Mvc;
using Market.Models;
using Microsoft.AspNetCore.Identity;
using Market.ViewModel;
using Market.Models.Services;
using Microsoft.AspNetCore.Authorization;

namespace Market.Controllers
{
    [Authorize(Roles = "User")]
    public class CartController : Controller
    {
        private readonly ICartRepo cartRepo = new CartRepo();
        private readonly UserManager<AppUser> _userManager;
        public CartController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<string> GetUserId()
        {
            var user = await _userManager.GetUserAsync(User);
            return user.Id;
        }
        public async Task<int> GetCartId()
        {
            var user = await _userManager.GetUserAsync(User);
            return cartRepo.GetCartId(user.Id);
        }
        public async Task<IActionResult> Index()
        {
            //here we show the list of items in the cart for the user
            string userId = await GetUserId();
            ViewBag.CartId = await GetCartId();
            return View(cartRepo.GetAll(userId));
        }
        
        public async Task<IActionResult> AddToCart(ProductDetails productDetails)
        {
            //here we add the product selected and it's quantity to the cart
            //we must decrease the quantity of the product in order 
            if (ModelState.IsValid)
            {
                string userId = await GetUserId();
                cartRepo.Add(productDetails, userId);
                cartRepo.Save();
                //return to products page so i can continue browsing
                return RedirectToAction("Index","Product");
            }
            return RedirectToAction("Details","Product",productDetails.product.Id);
        }
        public IActionResult RemoveFromCart(int Id)
        {
            //the Id here is for the cart Item
            cartRepo.Delete(Id);
            cartRepo.Save();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int Id)
        {
            return View(cartRepo.Get(Id));
        }
        public IActionResult SaveEdit(CartItem c)
        {
            cartRepo.Update(c);
            cartRepo.Save();
            return RedirectToAction("Index");
        }
        public IActionResult CheckOut()
        {
            //here we make the reciept
            return View();
        }
        public IActionResult SaveCheckOut()
        {
            //here we save the receipt and clear the Cart for the user
            return View();
        }
    }
}
