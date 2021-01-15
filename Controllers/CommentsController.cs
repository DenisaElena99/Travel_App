using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travel_App.Models;

using Travel_App.Models;

namespace Travel_App.Controllers
{
    public class CommentsController : Controller
    {
        private ApplicationDbContext _context;
        public CommentsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context = new ApplicationDbContext();

        }
        // GET: Comments
        public ActionResult Index()
        {
            return View();
        }

        [HttpDelete]
        [Authorize(Roles = "User,Editor,Admin")]
        public ActionResult Delete(int id)
        {
            Comment comm = _context.Comments.Find(id);
            if (comm.UserId == User.Identity.GetUserId() || User.IsInRole("Admin"))
            {
                _context.Comments.Remove(comm);
                _context.SaveChanges();
                return Redirect("/Articles/Details/" + comm.ArticleId);
            }
            else
            {
                TempData["message"] = "You don't have permission";
                return RedirectToAction("Index", "Articles");
            }
        }

        [HttpPost]
        public ActionResult New(Comment comm)
        {
            comm.Date = DateTime.Now;
            try
            {
                _context.Comments.Add(comm);
                _context.SaveChanges();
                return Redirect("/Articles/Details/" + comm.ArticleId);
            }

            catch (Exception e)
            {
                return Redirect("/Articles/Details/" + comm.ArticleId);
            }

        }

        [Authorize(Roles = "User,Editor,Admin")]
        public ActionResult Edit(int id)
        {
            Comment comm = _context.Comments.Find(id);

            if (comm.UserId == User.Identity.GetUserId() || User.IsInRole("Admin"))
            {
                return View(comm);
            }
            else
            {
                TempData["message"] = "You don't have permission";
                return RedirectToAction("Index", "Articles");
            }
        }

        [HttpPut]
        [Authorize(Roles = "User,Editor,Admin")]
        public ActionResult Edit(int id, Comment requestComment)
        {
            try
            {
                Comment comm = _context.Comments.Find(id);

                if (comm.UserId == User.Identity.GetUserId() || User.IsInRole("Admin"))
                {
                    if (TryUpdateModel(comm))
                    {
                        comm.Text = requestComment.Text;
                        _context.SaveChanges();
                    }
                    return Redirect("/Articles/Details/" + comm.ArticleId);
                }
                else
                {
                    TempData["message"] = "You don't have permission";
                    return RedirectToAction("Index", "Articles");
                }
            }
            catch (Exception e)
            {
                return View(requestComment);
            }
        }
    }
}