﻿using App_Dev.Data;
using App_Dev.Models;
using App_Dev.Utility;
using App_Dev.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App_Dev.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
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
        public IActionResult ChangePassword(string Id)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(n => n.Id == Id);
            UserVM userVM = new()
            {
                Id = user.Id,
                Password = user.PasswordHash,
            };
            return View(userVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(UserVM userVM)
        {
                
            var user = _db.ApplicationUsers.FirstOrDefault(n => n.Id == userVM.Id);
            _userManager.RemovePasswordAsync(user);
            _userManager.AddPasswordAsync(user, userVM.Password);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}