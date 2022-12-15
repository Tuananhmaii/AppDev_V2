using App_Dev.Data;
using App_Dev.Models;
using App_Dev.Repository.IRepository;
using App_Dev.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace App_Dev.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _db;
        public OrderController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult IndexAll()
        {
            IEnumerable<Order> orderList = _db.Orders;
            return View(orderList);
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            IEnumerable<Order> orderList = _db.Orders.Where(n => n.ApplicationUserId == claim.Value);
            return View(orderList);
        }

        [HttpGet]
        public IActionResult Details(int orderId)
        {
            OrderVM orderVM = new()
            {
                Order = _db.Orders.Find(orderId),
                OrderItem = _db.OrderItems.Include("Book").Where(n => n.OrderId == orderId)
            };
            return View(orderVM);
        }
    }
}
