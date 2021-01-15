using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travel_App.Models;

namespace Travel_App.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {

        private ApplicationDbContext _context;

        public CategoriesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context = new ApplicationDbContext();

        }
        // GET: Categories
        [Authorize(Roles = "User,Editor,Admin")]
        public ActionResult Index()
        {
            if (TempData.ContainsKey("message"))
            {
                ViewBag.message = TempData["message"].ToString();
            }

            var categories = from category in _context.Categories
                             orderby category.CategoryName
                             select category;
            ViewBag.Categories = categories;
            return View();
        }

        [Authorize(Roles = "User,Editor,Admin")]
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                Category category = _context.Categories.Find(id);

                if (category != null)
                {
                    return View(category);
                }
                return HttpNotFound("Couldn't find the category id " + id.ToString());
            }
            return HttpNotFound("Missing category id!");
        }


        [Authorize(Roles = "Editor,Admin")]
        public ActionResult New()
        {
            return View();
        }


        [Authorize(Roles = "Editor,Admin")]
        [HttpPost]
        public ActionResult New(Category cat)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Categories.Add(cat);
                    _context.SaveChanges();
                    TempData["message"] = "Category has been added!";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(cat);
                }
            }
            catch (Exception e)
            {
                return View(cat);
            }
        }


        [Authorize(Roles = "Editor,Admin")]
        public ActionResult Edit(int id)
        {
            Category category = _context.Categories.Find(id);
            return View(category);
        }

        [Authorize(Roles = "Editor,Admin")]
        [HttpPut]
        public ActionResult Edit(int id, Category requestCategory)
        {
            try
            {
                Category category = _context.Categories.Find(id);

                //throw new Exception();

                if (TryUpdateModel(category))
                {
                    category.CategoryName = requestCategory.CategoryName;
                    _context.SaveChanges();
                    TempData["message"] = "Category has been modified!";
                    return RedirectToAction("Index");
                }

                return View(requestCategory);
            }
            catch (Exception e)
            {
                return View(requestCategory);
            }
        }

        [Authorize(Roles = "Editor,Admin")]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Category category = _context.Categories.Find(id);
            _context.Categories.Remove(category);
            TempData["message"] = "Categoria a fost stearsa!";
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}