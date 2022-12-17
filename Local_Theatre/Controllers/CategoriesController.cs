using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Local_Theatre.Models;

namespace Local_Theatre.Controllers
{
    [Authorize(Roles = "SysAdmin")]
    public class CategoriesController : ApplicationBaseController
    {
        private LocalTheatreDbContext db = new LocalTheatreDbContext();

        // GET: Categories
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,CategoryName")] Category category)
        {
            //if model is valid, add new category and return back to Index
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //else return back to create view
            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //find category by id
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            //return the view with the category
            return View(category);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,CategoryName")] Category category)
        {
            //if model is valid, edit the category and return back to Index
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //else, return back to edit view
            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //find the category by id
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            //remove the category and return back to Index
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
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
