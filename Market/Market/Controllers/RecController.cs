using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Market.Models;
using Market.Models.Services;
using Microsoft.AspNetCore.Authorization;

namespace Market.Controllers
{
    [Authorize(Roles = "User")]
    public class RecController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IRecRepo recRepo = new RecRepo();
        public RecController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<string> GetUserId()
        {
            var user = await userManager.GetUserAsync(User);
            return user.Id;
        }
        public async Task<IActionResult> Index()
        {
            //show All recipets here
            //we get the userId
            string UserId = await GetUserId();
            return View(recRepo.GetAll(UserId));
        }
        public IActionResult Add(int Id)
        {
            recRepo.Add(Id);
            recRepo.Save();
            return RedirectToAction("Index");
        }
    }
}
