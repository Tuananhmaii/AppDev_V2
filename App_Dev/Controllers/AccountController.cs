using App_Dev.Data;
using App_Dev.Models;
using App_Dev.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App_Dev.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountController(ApplicationDbContext db, UserManager<ApplicationUser> userManager) 
        { 
            _db = db; 
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            IEnumerable<ApplicationUser> userList = _db.ApplicationUsers;
            return View(userList);
        }
        public async Task<IActionResult> CustomerAccount()
        {
            IEnumerable<IdentityUser> customerList = await _userManager.GetUsersInRoleAsync(SD.Customer_Role);
            return View(customerList);
        }
        public async Task<IActionResult> AdminAccount()
        {
            IEnumerable<IdentityUser> customerList = await _userManager.GetUsersInRoleAsync(SD.Admin_Role);
            return View(customerList);
        }
        public async Task<IActionResult> StoreAccount()
        {
            IEnumerable<IdentityUser> storeList = await _userManager.GetUsersInRoleAsync(SD.Store_Role);
            return View(storeList);
        }
        [HttpGet]
        public IActionResult ChangePassword(string accountId)
        {
            var user = _db.ApplicationUsers.Find(accountId);
            return View(user);
        }
        [HttpPost]
        [ActionName("ChangePassword")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePasswordPOST(ApplicationUser user, string password)
        {

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, token, password);
            return View(user);
        }
    }
}
