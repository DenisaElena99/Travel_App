
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travel_App.Models;

namespace Travel_App.Controllers
{
    [Authorize]
    public class ArticlesController : Controller
    {
        private ApplicationDbContext _context;

        public ArticlesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context = new ApplicationDbContext();

        }

        // GET: Articles
        [HttpGet]
        [AllowAnonymous]

        public ActionResult Index()
        {
            var articles = _context.Articles.Include("Categories").Include("User").Include("Destination").OrderBy(a => a.Date).ToList();
            ViewBag.Articles = articles;

            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"];
            }

            return View();
        }


        [HttpGet]
        [Authorize(Roles = "User,Editor,Admin")]
        public ActionResult Details(int? id)
        {
            Article article = _context.Articles.Find(id);

            SetAccessRights();

            return View(article);
        }

        [Authorize(Roles = "User,Editor,Admin")]
        public ActionResult Details(Comment comment)
        {

            comment.Date = DateTime.Now;
            comment.UserId = User.Identity.GetUserId();
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Comments.Add(comment);
                    _context.SaveChanges();
                    return Redirect("/Articles/Details/" + comment.ArticleId);
                }

                else
                {
                    Article a = _context.Articles.Find(comment.ArticleId);

                    SetAccessRights();

                    return View(a);
                }
            }

            catch (Exception e)
            {
                Article a = _context.Articles.Find(comment.ArticleId);

                SetAccessRights();

                return View(a);
            }
        }

        [Authorize(Roles = "User,Editor,Admin")]
        public ActionResult DetailsOwner(Comment comment)
        {
            comment.Date = DateTime.Now;
            comment.UserId = User.Identity.GetUserId();
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Comments.Add(comment);
                    _context.SaveChanges();
                    return Redirect("/Articles/Details/" + comment.ArticleId);
                }

                else
                {
                    Article a = _context.Articles.Find(comment.ArticleId);

                    SetAccessRights();

                    return View(a);
                }
            }

            catch (Exception e)
            {
                Article a = _context.Articles.Find(comment.ArticleId);

                SetAccessRights();

                return View(a);
            }
        }


        [HttpGet]
        [Authorize(Roles = "Editor,Admin")]
        public ActionResult New()
        {
            Article article = new Article();
           // article.CategoriesList = GetAllCategories();
            article.CategoriesList = GetAllCategories();
            article.UserId = User.Identity.GetUserId();
            return View(article);
        }

        [HttpPost]
        [Authorize(Roles = "Editor,Admin")]
        public ActionResult New(Article article)
        {
            article.Date = DateTime.Now;
            article.UserId = User.Identity.GetUserId();
            var selectedCategories = article.CategoriesList.Where(a => a.Checked).ToList();
            try
            {
                if (ModelState.IsValid)
                {
                    article.Categories = new List<Category>();


                    for (int i = 0; i < selectedCategories.Count(); i++)
                    {
                        Category category = _context.Categories.Find(selectedCategories[i].Id);
                        article.Categories.Add(category);
                    }
                    _context.Articles.Add(article);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    article.CategoriesList = GetAllCategories();
                    return View(article);
                }
            }
            catch (Exception e)
            {
                article.CategoriesList = GetAllCategories();
                return View(article);
            }
        }

        [Authorize(Roles = "Editor,Admin")]
        public ActionResult Edit(int? id)
        {
            
               Article article = _context.Articles.Find(id);
               article.CategoriesList = GetAllCategories();

               if (article.UserId == User.Identity.GetUserId() || User.IsInRole("Admin"))
               {
                   return View(article);
               }

               else
               {
                   TempData["message"] = "You don't have permission";
                   return RedirectToAction("Index");
               }
            
        }

        [HttpPut]
        [Authorize(Roles = "Editor,Admin")]

        public ActionResult Edit(int id, Article articleRequest)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    Article article = _context.Articles.Find(id);

                    if (article.UserId == User.Identity.GetUserId() || User.IsInRole("Admin"))
                    {
                        if (TryUpdateModel(article))
                        {
                            article.Title = articleRequest.Title;
                            article.Text = articleRequest.Text;
                            article.CategoryId = articleRequest.CategoryId;
                            _context.SaveChanges();
                            TempData["message"] = "The article has been modified";
                        }
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["message"] = "You don't have permission";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    articleRequest.CategoriesList = GetAllCategories();
                    return View(articleRequest);
                }
            }
            catch (Exception e)
            {
                articleRequest.CategoriesList = GetAllCategories();
                return View(articleRequest);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Editor,Admin")]

        public ActionResult Delete(int id)
        {
            Article article = _context.Articles.Find(id);

            if (article.UserId == User.Identity.GetUserId() || User.IsInRole("Admin"))
            {
                _context.Articles.Remove(article);
                _context.SaveChanges();
                TempData["message"] = "Deleted article";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = "You don't have permission";
                return RedirectToAction("Index");
            }
        }


        [NonAction]
        public List<CheckBoxViewModel> GetAllCategories()
        {
            var checkboxList = new List<CheckBoxViewModel>();
            foreach (var category in _context.Categories.ToList())
            {
                checkboxList.Add(new CheckBoxViewModel
                {
                    Id = category.CategoryId,
                    Name = category.CategoryName,
                    Checked = false
                });
            }
            return checkboxList;
        }


        [NonAction]
        public IEnumerable<SelectListItem> GetAllComments()
        {
            var selectList = new List<SelectListItem>();
            foreach (var comment in _context.Comments.ToList())
            {
                selectList.Add(new SelectListItem
                {
                    Value = comment.CommentId.ToString(),
                    Text = comment.Text
                });
            }
            return selectList;
        }

        private void SetAccessRights()
        {
            ViewBag.afisareButoane = false;
            if (User.IsInRole("Editor") || User.IsInRole("Admin"))
            {
                ViewBag.afisareButoane = true;
            }

            ViewBag.esteAdmin = User.IsInRole("Admin");
            ViewBag.utilizatorCurent = User.Identity.GetUserId();
        }
    }
}