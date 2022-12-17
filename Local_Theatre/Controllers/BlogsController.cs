using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Local_Theatre.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;

namespace Local_Theatre.Controllers
{
    public class BlogsController : ApplicationBaseController
    {
        private LocalTheatreDbContext db = new LocalTheatreDbContext();

        public ActionResult Index(string Search, int? page)
        {
            //if user is not signed in or a member or a suspended
            if (!Request.IsAuthenticated || User.IsInRole("Member") || User.IsInRole("Suspended"))
            {
                //if Search string is null
                if (String.IsNullOrEmpty(Search))
                {
                    //return all approved blogs to the view
                    return View(db.Blogs
                         .Include(b => b.Category)
                         .Include(b => b.Staff)
                         .Include(b => b.Comments)
                         .Where(b => b.BlogApproved == true)
                         .OrderBy(b => b.BlogId)
                         .ToList()
                         .ToPagedList(page ?? 1, 3));
                }
                //else, return all approved blogs where blog title contains search string
                else
                {
                    return View(db.Blogs
                        .Include(b => b.Category)
                        .Include(b => b.Staff)
                        .Include(b => b.Comments)
                        .Where(b => b.BlogApproved == true)
                        .Where(p => p.BlogTitle.Contains(Search.Trim()))
                        .OrderByDescending(p => p.BlogPosted)
                        .ToList()
                        .ToPagedList(page ?? 1, 3));
                }
            }
            //if seacrh string is null and user is a staff, return all blogs to the view
            if (String.IsNullOrEmpty(Search))
            {
                return View(db.Blogs
                    .Include(b => b.Category)
                    .Include(b => b.Staff)
                    .Include(b => b.Comments)
                    .OrderBy(b => b.BlogId)
                    .ToList()
                    .ToPagedList(page ?? 1, 3));
            }
            //else, return all blogs which contain search string.
            return View(db.Blogs
                    .Include(b => b.Category)
                    .Include(b => b.Staff)
                    .Include(b => b.Comments)
                    .Where(p => p.BlogTitle.Contains(Search.Trim()))
                    .OrderByDescending(p => p.BlogPosted)
                    .ToList()
                    .ToPagedList(page ?? 1, 3));
        }

        [Authorize]
        public ActionResult MyBlogs(string id, int? page) {
            //returning all blogs which have user id = to id passed in as a parameter
            return View("Index", db.Blogs
                    .Include(b => b.Category)
                    .Include(b => b.Staff)
                    .Include(b => b.Comments)
                    .Where(b => b.UserId == id)
                    .ToList()
                    .ToPagedList(page ?? 1, 3));
        }

        public ActionResult Category(string Search, int? page, int? id) {
            //if id is null, return badRequest error
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //else, add all categories to ViewBag
            ViewBag.Categories = db.Categories.ToList();
            //if search string is null, return all blogs whos category id = to parameter id
            if (String.IsNullOrEmpty(Search))
            {
                return View("Index", db.Blogs
                    .Where(b => b.CategoryId == id)
                    .Where(b => b.BlogApproved)
                    .Include(b => b.Category)
                    .Include(b => b.Staff)
                    .Include(b => b.Comments)
                    .ToList()
                    .ToPagedList(page ?? 1, 3));
            }
            //else, return all blogs whos category id = to parameter id and blog title = to seacrch string
            return View("Index", db.Blogs
                    .Where(b => b.CategoryId == id)
                    .Where(b => b.BlogApproved)
                    .Where(p => p.BlogTitle.Contains(Search.Trim()))
                    .Include(b => b.Category)
                    .Include(b => b.Staff)
                    .Include(b => b.Comments)
                    .ToList()
                    .ToPagedList(page ?? 1, 3));
        }

        public ActionResult Blog(int? id)
        {
            //if id is null, return badRequest error
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //else, find the first blog whos id = to parameter id, include category, staff, comment and assign it to blog varable
            var blog = db.Blogs
                    .Include(b => b.Category)
                    .Include(b => b.Staff)
                    .Include(b => b.Comments)
                    .FirstOrDefault(x => x.BlogId == id);

            if (blog == null)
            {
                return HttpNotFound();
            }
            //creating varaible count to hold a number of approved blog where blog id = to parameter id
            int count = db.Comments.Where(b => b.BlogId == blog.BlogId && b.CommentApproved).Count();
            //asign the count variable to ViewBag Count
            ViewBag.Count = count;
            ViewBag.Blog = blog;
            //return the blog to the view
            return View();
        }

        // GET: Blogs/Create
        [Authorize]
        public ActionResult Create()
        {
            //passing two viewBags to Create view, Category and User
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }

        // POST: Blogs/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BlogId,BlogTitle,BlogContext,CategoryId")] Blog blog)
        {
            //finding the currently signed in user
            UserManager<User> userManager = new UserManager<User>(new UserStore<User>(db));
            string userId = userManager.FindByEmail(User.Identity.GetUserName()).Id;
            User user = db.Users.Find(userId);

            //assigning missing values of the blog
            blog.UserId = user.Id;
            blog.BlogPosted = DateTime.Now;
            blog.BlogApproved = false;
            //if model is valid, adding blog to the database and returning back to Blog
            if (ModelState.IsValid)
            {
                db.Blogs.Add(blog);
                db.SaveChanges();

                return RedirectToAction("Blog", "Blogs", new { id = blog.BlogId });
            }
            //else, returning back to Create action
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", blog.CategoryId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", blog.UserId);
            return View(blog);
        }

        // GET: Blogs/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            //if id is null, show an error
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //else, find the blog by Id
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            //passing blog to the Edit view
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", blog.CategoryId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", blog.UserId);
            return View(blog);
        }

        // POST: Blogs/Edit/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BlogId,BlogTitle,BlogContext,CategoryId,BlogPosted,UserId,BlogApproved")] Blog blog)
        {
            //if model is valid, updating the blog and returing back to Index
            if (ModelState.IsValid)
            {
                db.Entry(blog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //else, returning blog back to Edit View
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", blog.CategoryId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", blog.UserId);
            return View(blog);
        }

        //GET: Blogs/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            //if id is null, show an error
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //else, find the first blogs whos id = to parameter id
            var blog = db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            //if the blog is not null, remove the blog and return back to index
            db.Blogs.Remove(blog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult ApproveBlog(int? id, int? page)
        {
            //find the blog by id, set the approve property to true and return back to Index View
            Blog blog = db.Blogs.Find(id);

            blog.BlogApproved = true;
            db.SaveChanges();

            return RedirectToAction("Index", new { page });
        }

        [Authorize]
        public ActionResult DisapproveBlog(int? id, int? page)
        {
            //find the blog by id, set the approve property to false and return back to Index View
            Blog blog = db.Blogs.Find(id);

            blog.BlogApproved = false;
            db.SaveChanges();

            return RedirectToAction("Index", new { page} );
        }

        public ActionResult ApproveAllBlogs()
        {
            //finding all uapproved blogs
            var blogs = db.Blogs.Where(b => b.BlogApproved == false).ToList();
            //for each unapproved blog, set approve property to true
            foreach (var item in blogs)
            {
                item.BlogApproved = true;
            }
            db.SaveChanges();
            //save and return back to ModerateBlogs View
            return RedirectToAction("ModerateBlogs", "Blogs", new { unapproved = true });
        }

        public ActionResult DeteleAllBlogs()
        {
            //finding all approved blogs
            var blogs = db.Blogs.Where(b => b.BlogApproved == false).ToList();
            //for each unapproved blog, remove the blog
            foreach (var item in blogs)
            {
                db.Blogs.Remove(item);
            }
            db.SaveChanges();
            //save and return back to ModerateBlogs View
            return RedirectToAction("ModerateBlogs", "Blogs", new { unapproved = true });
        }

        //only allowing Mod to access this action
        [Authorize(Roles = "Moderator")]
        public ActionResult ModerateBlogs(bool unapproved)
        {
            //if unapproved is true, return all unapproved blogs to the view
            if (unapproved == true) { 
                return View(db.Blogs
                        .Where(b => b.BlogApproved == false)
                        .Include(b => b.Category)
                        .Include(b => b.Staff)
                        .Include(b => b.Comments)
                        .ToList());
            }
            //else, return all the blogs
            return View(db.Blogs
                    .Include(b => b.Category)
                    .Include(b => b.Staff)
                    .Include(b => b.Comments)
                    .ToList());
        }

        [Authorize]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
