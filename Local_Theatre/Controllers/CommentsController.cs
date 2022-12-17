using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Local_Theatre.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Local_Theatre.Controllers
{
    [Authorize]
    public class CommentsController : ApplicationBaseController
    {
        private LocalTheatreDbContext db = new LocalTheatreDbContext();

        // POST: Comments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommentId,CommentContext,BlogId")] Comment comment)
        {
            //finding the currently signed in user
            UserManager<User> userManager = new UserManager<User>(new UserStore<User>(db));
            string userId = userManager.FindByEmail(User.Identity.GetUserName()).Id;
            User user = db.Users.Find(userId);
            //fining the blog using BlogId property of comment
            var blog = db.Blogs.Find(comment.BlogId);
            //if user is Mod, SysAmin or blog belogns to user, setting coment to approved, else to unapproved
            if (user.CurrentRole == "Moderator" || user.CurrentRole == "SysAdmin" || blog.UserId == user.Id)
            {
                comment.CommentApproved = true;
            }
            else
            {
                comment.CommentApproved = false;
            }
            //settign the missing values 
            comment.CommentPosted = DateTime.Now;
            comment.UserId = user.Id;
            //if model is valid, adding comment to database and returing to Blog View
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Blog", "Blogs", new { id = comment.BlogId });
            }

            return RedirectToAction("Blog", "Blogs", new { id = comment.BlogId });
        }

        //GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //finding the comment by Id
            Comment comment = db.Comments.Find(id);

            if (comment == null)
            {
                return HttpNotFound();
            }
            //if comment is not null, removing the comment and returnig back to the View
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Blog", "Blogs", new { id = comment.BlogId });
        }


        public ActionResult ApproveComment(int? id) {
            //finding the comment by id, setting the approved to true and returnign back to the Blog View
            Comment comment = db.Comments.Find(id);

            comment.CommentApproved = true;
            db.SaveChanges();

            return RedirectToAction("Blog", "Blogs", new { id = comment.BlogId });
        }

        public ActionResult DisapproveComment(int? id)
        {
            //finding the comment by id, setting the approved to false and returnign back to the Blog View
            Comment comment = db.Comments.Find(id);

            comment.CommentApproved = false;
            db.SaveChanges();

            return RedirectToAction("Blog", "Blogs", new { id = comment.BlogId });
        }

        public ActionResult ApproveAllComments()
        {
            //finding al unapproved comments
            var comments = db.Comments.Where(b => b.CommentApproved == false).ToList();

            //for each comment setting the approved to true and returnign back to the Blog View
            foreach (var item in comments) 
            {
                item.CommentApproved = true;
            }
            db.SaveChanges();

            return RedirectToAction("ModerateComments", "Admin");
        }

        public ActionResult DeteleAllComments()
        {
            //finding al unapproved comments
            var comments = db.Comments.Where(b => b.CommentApproved == false).ToList();
            //for each comment removing the comment and returnign back to the Blog View
            foreach (var item in comments)
            {
                db.Comments.Remove(item);
            }
            db.SaveChanges();

            return RedirectToAction("ModerateComments", "Admin");
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
