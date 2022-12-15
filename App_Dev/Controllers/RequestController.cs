using App_Dev.Data;
using App_Dev.Models;
using App_Dev.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace App_Dev.Controllers
{
    public class RequestController : Controller
    {
        private readonly ApplicationDbContext _db;
        public RequestController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Request> requestList = _db.Requests.Include("ApplicationUser");
            return View(requestList);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Request request)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            request.ApplicationUserId = claim.Value;
            request.Status = "Waiting";
            _db.Requests.Add(request);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int requestId)
        {
            Request request = _db.Requests.FirstOrDefault(n=>n.Id== requestId);
            return View(request);
        }
        [HttpPost]
        public IActionResult Edit(Request request)
        {
            _db.Requests.Update(request);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
