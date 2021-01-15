using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travel_App.Models;

namespace Travel_App.Controllers
{
    public class DestinationsController : Controller
    {
        private ApplicationDbContext _context;

        public DestinationsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context = new ApplicationDbContext();
        }
        // GET: Destinations

        [HttpGet]
        public ActionResult Index()
        {
            List<Destination> destinations = _context.Destinations.ToList();
            return View(destinations);
        }
        [HttpGet]
        public ActionResult New()
        {
            Destination destination = new Destination();
            return View(destination);
        }

        [HttpPost]
        public ActionResult New(Destination destinationRequest)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    _context.Destinations.Add(destinationRequest);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Article");
                }
                return View(destinationRequest);
            }
            catch (Exception e)
            {
                return View(destinationRequest);
            }
        }


    }

    //public ActionResult Edit(int id, Article articleRequest)
    //{
    //    articleRequest.CommentList = GetAllComments();

    //    // preluam articolul pe care vrem sa il modificam din baza de date
    //    Article article = _context.Article.Include("Comment")
    //                .SingleOrDefault(a => a.ArticleId.Equals(id));

    //    // memoram intr-o lista doar genurile care au fost selectate din formular
    //    var selectedCategories = articleRequest.Where(a => a.Checked).ToList();
    //    try
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            if (TryUpdateModel(article))
    //            {
    //                article.Title = articleRequest.Title;
    //                article.Text = articleRequest.Text;
    //                article.Category.Clear();
    //                article.Category = new List<Category>();

    //                for (int i = 0; i < selectedCategories.Count(); i++)
    //                {
    //                    // articolului pe care vrem sa il editam ii asignam genurile selectate 
    //                    Category category = _context.Categories.Find(selectedCategories[i].Id);
    //                    article.Category.Add(category);
    //                }
    //                _context.SaveChanges();
    //            }
    //            return RedirectToAction("Index");
    //        }
    //        return View(articleRequest);
    //    }
    //    catch (Exception)
    //    {
    //        return View(articleRequest);
    //    }

    //    [NonAction]
    //    public IEnumerable<SelectListItem> GetAllComments()
    //    {
    //        var selectList = new List<SelectListItem>();
    //        foreach (var comment in _context.Comments.ToList())
    //        {
    //            selectList.Add(new SelectListItem
    //            {
    //                Value = comment.CommentId.ToString(),
    //                Text = comment.Text
    //            });
    //        }
    //        return selectList;
    //    }
    //}
}