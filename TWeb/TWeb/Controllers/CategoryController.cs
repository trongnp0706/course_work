using Microsoft.AspNetCore.Mvc;
using DrsfanBook.DataAcess.Data;
using DrsfanBook.DataAcess.Repository.IRepository;
using DrsfanBook.Models;


namespace DrsfanBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;
        public CategoryController(ICategoryRepository db)
        {
            _categoryRepo = db;
        }
        public IActionResult Index()
        {
            List<Category> ojbCategoryList = _categoryRepo.GetAll().ToList();
            return View(ojbCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _categoryRepo.Add(obj);
                _categoryRepo.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDB = _categoryRepo.Get(u=>u.Id == id);
            //Category? categoryFromDB1 = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //Category? categoryFromDB2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();

            if (categoryFromDB == null)
            {
                return NotFound();
            }

            return View(categoryFromDB);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            
            if (ModelState.IsValid)
            {
                _categoryRepo.Update(obj);
                _categoryRepo.Save();
                TempData["success"] = "Category update successfully";
                return RedirectToAction("Index");
            }
            return View();

        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDB = _categoryRepo.Get(u => u.Id == id);
            //Category? categoryFromDB1 = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //Category? categoryFromDB2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();

            if (categoryFromDB == null)
            {
                return NotFound();
            }

            return View(categoryFromDB);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int id)
        {
            Category? obj = _categoryRepo.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            
            _categoryRepo.Remove(obj);
            _categoryRepo.Save();
            TempData["success"] = "Category delete successfully";
            return RedirectToAction("Index");

        }


    }
}
