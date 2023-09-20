
using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.Irepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;


namespace BulkyBookWeb.Areas.Admin.Controllers;
[Area("Admin")]

    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitofwork;

        public CategoryController(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> ObjCategoryList = _unitofwork.Category.GetAll();
            return View(ObjCategoryList);
        }

        // GET: Category/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name.");
            }

            if (ModelState.IsValid)
            {
                _unitofwork.Category.Add(obj);
                _unitofwork.save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        // GET: Category/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //var categoryFromDb = _db.Categories.Find(id);
            var CategoryFromDbFirst = _unitofwork.Category.GetFirstOrDefault(u => u.Id == id);
            //var CategoryFromDbSingle=_db.Categories.SingleOrDefault(u=>u.Id=id);
            if (CategoryFromDbFirst == null)
            {
                return NotFound();
            }

            return View(CategoryFromDbFirst);
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name.");
            }

            if (ModelState.IsValid)
            {
                _unitofwork.Category.Update(obj);
                _unitofwork.save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        // GET: Category/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var categoryFromDb = _db.Categories.Find(id);
            var CategoryFromDbFirst = _unitofwork.Category.GetFirstOrDefault(u => u.Id == id);
            //var CategoryFromDbSingle=_db.Categories.SingleOrDefault(u=>u.Id=id);

            if (CategoryFromDbFirst == null)
            {
                return NotFound();
            }

            return View(CategoryFromDbFirst);
        }

        // POST: Category/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int id)
        {
            var categoryFromDb = _unitofwork.Category.GetFirstOrDefault(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            _unitofwork.Category.Remove(categoryFromDb);
            _unitofwork.save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
