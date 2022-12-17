using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Local_Theatre.Models;
using Microsoft.AspNet.Identity;
using MvcSiteMapProvider.Linq;

namespace Local_Theatre.Controllers
{
    public class HomeController : ApplicationBaseController
    {
        private LocalTheatreDbContext db = new LocalTheatreDbContext();

        public ActionResult Index()
        {
            //taking 1 blog where category = Announcments, ordered by descening date and returing it to the View as a ViewBag
            var recent = db.Blogs
                .Include(b => b.Category)
                .Include(b => b.Staff)
                .Where(b => b.BlogApproved && b.Category.CategoryName == "Announcments")
                .OrderByDescending(b => b.BlogPosted)
                .Take(1)
                .ToList();

            ViewBag.Recent = recent;
            //returning all categories to the View
            return View(db.Categories.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact Us.";
            return View();
        }

        public ActionResult RedirectView() {
            return View();
        }
    }
}