using App_Dev.Models;
using App_Dev.Repository.IRepository;
using App_Dev.Utility;
using App_Dev.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace App_Dev.Controllers
{
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public int OrderTotal { get; set; }
        [BindProperty]
        public ShoppingCartVM shoppingCartVM { get; set; }
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCartVM = new ShoppingCartVM()
            {
                ListCart = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value,
                includeProperties: "Book"),
                Order = new()
            };
            foreach (var item in shoppingCartVM.ListCart)
            {
                item.Price = item.Book.Price;
                shoppingCartVM.Order.TotalPrice += (item.Price * item.Count);
            }
            return View(shoppingCartVM);
        }
    }
}
