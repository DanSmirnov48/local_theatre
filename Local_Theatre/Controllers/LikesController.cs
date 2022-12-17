using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Local_Theatre.Models;
using Microsoft.AspNet.Identity;
using PagedList;

namespace Local_Theatre.Controllers
{
    [Authorize]
    public class LikesController : ApplicationBaseController
    {
        private LocalTheatreDbContext db = new LocalTheatreDbContext();

        public ActionResult MyLikedPosts(string id, int? page) 
        {
            //if id is null, retunrning an error message
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //else, taking all likes where user id = parameter id value and sending it to the View
            return View(db.Likes
                    .Include(l => l.Blog)
                    .Include(l => l.User)
                    .Where(l => l.UserId == id)
                    .OrderBy(l => l.LikeId)
                    .ToList()
                    .ToPagedList(page ?? 1, 3));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LikeId,BlogId,UserId")] Like like, int? page)
        {
            //if model is valid, adding the like object toi the database and returing back to the View
            if (ModelState.IsValid)
            {
                db.Likes.Add(like);
                db.SaveChanges();
                return Redirect(Url.Action("Index", "Blogs", new { page }) + "#"+like.BlogId);
            }
            return View(like);
        }

        public ActionResult Delete(int? id, int? page)
        {
            //if id is null, returning an error message
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //finid the like by an id
            Like like = db.Likes.Find(id);
            if (like == null)
            {
                return HttpNotFound();
            }
            //removing the like and retunrning back to the View
            db.Likes.Remove(like);
            db.SaveChanges();

            return Redirect(Url.Action("Index", "Blogs", new { page }) + "#" + like.BlogId);
        }

        public ActionResult Delete1(int? id, int? page)
        {
            //same thing but returns to a different view, I know this is bad design by i cound't find a way to send to one or another view (bool was always returning null)
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Like like = db.Likes.Find(id);
            if (like == null)
            {
                return HttpNotFound();
            }
            db.Likes.Remove(like);
            db.SaveChanges();

            return Redirect(Url.Action("MyLikedPosts", "Likes", new { id = User.Identity.GetUserId(), page = 1  /*remove "= 1"*/}));
        }

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