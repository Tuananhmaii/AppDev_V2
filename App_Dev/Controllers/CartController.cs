using App_Dev.Models;
using App_Dev.Repository.IRepository;
using App_Dev.Utility;
using App_Dev.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace App_Dev.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartVM shoppingCartVM { get; set; }
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ShoppingCartVM shoppingCartVM = new ShoppingCartVM()
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

        [HttpGet]
        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ShoppingCartVM shoppingCartVM = new ShoppingCartVM()
            {
                ListCart = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value,
                includeProperties: "Book"),
                Order = new()
            };
            shoppingCartVM.Order.ApplicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(
                u => u.Id == claim.Value);
            shoppingCartVM.Order.Name = shoppingCartVM.Order.ApplicationUser.FullName;
            shoppingCartVM.Order.PhoneNumber = shoppingCartVM.Order.ApplicationUser.PhoneNumber;
            shoppingCartVM.Order.City = shoppingCartVM.Order.ApplicationUser.City;
            shoppingCartVM.Order.Address = shoppingCartVM.Order.ApplicationUser.Address;
            foreach (var item in shoppingCartVM.ListCart)
            {
                item.Price = item.Book.Price;
                shoppingCartVM.Order.TotalPrice += (item.Price * item.Count);
            }
            return View(shoppingCartVM);
        }

        [HttpPost]
        [ActionName("Summary")]
        [ValidateAntiForgeryToken]
        public IActionResult SummaryPOST(ShoppingCartVM shoppingCartVM)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCartVM.ListCart = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value,
                includeProperties: "Book");

            shoppingCartVM.Order.DayCreated = DateTime.Now;
            shoppingCartVM.Order.ApplicationUserId = claim.Value;

            shoppingCartVM.Order.ApplicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(
                u => u.Id == claim.Value);
            shoppingCartVM.Order.Name = shoppingCartVM.Order.ApplicationUser.FullName;
            shoppingCartVM.Order.PhoneNumber = shoppingCartVM.Order.ApplicationUser.PhoneNumber;
            shoppingCartVM.Order.City = shoppingCartVM.Order.ApplicationUser.City;
            shoppingCartVM.Order.Address = shoppingCartVM.Order.ApplicationUser.Address;

            foreach (var item in shoppingCartVM.ListCart)
            {
                item.Price = item.Book.Price;
                shoppingCartVM.Order.TotalPrice += (item.Price * item.Count);
            }

            ApplicationUser applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value);
            _unitOfWork.Order.Add(shoppingCartVM.Order);
            _unitOfWork.Save();
            foreach (var item in shoppingCartVM.ListCart)
            {
                OrderItem orderItem = new()
                {
                    OrderId = shoppingCartVM.Order.Id,
                    BookId = item.BookId,
                    Count = item.Count,
                    Price = item.Price,
                };
                _unitOfWork.OrderItem.Add(orderItem);
                _unitOfWork.Save();
            }
            return RedirectToAction("OrderConfirmation", "Cart", new { id = shoppingCartVM.Order.Id });
        }

        public IActionResult OrderConfirmation(int id)
        {
            Order order = _unitOfWork.Order.GetFirstOrDefault(u => u.Id == id, includeProperties: "ApplicationUser");

            List<ShoppingCart> shoppingCarts = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId ==
            order.ApplicationUserId).ToList();
            _unitOfWork.ShoppingCart.RemoveRange(shoppingCarts);
            _unitOfWork.Save();
            return View(id);
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
            return RedirectToAction("Index");
        }
    }
}
