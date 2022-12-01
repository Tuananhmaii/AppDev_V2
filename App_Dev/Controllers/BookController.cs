using App_Dev.Data;
using App_Dev.Models;
using App_Dev.Repository.IRepository;
using App_Dev.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Hosting;

namespace App_Dev.Controllers
{
    public class BookController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public BookController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Book> objBookList = _unitOfWork.Book.GetAll();
            return View(objBookList);
        }

        //GET
        [HttpGet]
        public IActionResult Create()
        {
            BookVM bookVM = new BookVM()
            {
                Book = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                })
            };
            return View(bookVM);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookVM obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Book.Add(obj.Book);
                _unitOfWork.Save(); ;
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            BookVM bookVM = new BookVM()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                })
            };

            if (id == null || id == 0)
            {
                return View(bookVM);
            }
            else
            {
                bookVM.Book = _unitOfWork.Book.GetFirstOrDefault(u => u.Id == id);
                return View(bookVM);
            }
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BookVM obj, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file.FileName);

                    if (obj.Book.Image != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.Book.Image.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    obj.Book.Image = @"images\products\" + fileName + extension;
                }
                _unitOfWork.Book.Update(obj.Book);
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
