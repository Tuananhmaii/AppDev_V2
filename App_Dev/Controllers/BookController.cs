using App_Dev.Data;
using App_Dev.Models;
using App_Dev.Repository.IRepository;
using App_Dev.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App_Dev.Controllers
{
    public class BookController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Book> objBookList = _unitOfWork.Book.GetAll();
            return View(objBookList);
        }

        //GET
        public IActionResult Create()
        {
            BookVM book = new BookVM()
            {
                Book = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                })
            };
            return View(book);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book obj)
        {
            Book book = new();
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                });
            ViewBag.CategoryList = CategoryList;
            if (ModelState.IsValid)
            {
                _unitOfWork.Book.Add(obj);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        GET
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var bookFromDbFirst = _unitOfWork.Book.GetFirstOrDefault(u => u.Id == id);
            if(bookFromDbFirst == null)
            {
                return NotFound();
            }

            return View(bookFromDbFirst);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Book obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Book.Update(obj);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var bookFromDbFirst = _unitOfWork.Book.GetFirstOrDefault(u => u.Id == id);
            if (bookFromDbFirst == null)
            {
                return NotFound();
            }

            return View(bookFromDbFirst);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.Book.GetFirstOrDefault(u => u.Id == id);
            if(obj == null)
            {
                return NotFound();
            }
            
            _unitOfWork.Book.Remove(obj);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
