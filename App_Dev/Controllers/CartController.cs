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




        public IActionResult Plus(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
            _unitOfWork.ShoppingCart.IncrementCount(cart, 1);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
        public IActionResult Minus(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
            if (cart.Count > 1)
            {
                _unitOfWork.ShoppingCart.DecrementCount(cart, 1);
                var count = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count - 1;
            }
            else
            {
                _unitOfWork.ShoppingCart.Remove(cart);
            }
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
        public IActionResult Remove(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
            _unitOfWork.ShoppingCart.Remove(cart);
            _unitOfWork.Save();
            var count = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count;
            return RedirectToAction("Index");
        }
    }
}
