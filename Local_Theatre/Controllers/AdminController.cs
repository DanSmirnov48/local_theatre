using System;
using System.Linq;
using System.Web.Mvc;
using Local_Theatre.Models;
using Local_Theatre.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System.Net;
using System.Threading.Tasks;
using System.Data.Entity;
using MvcSiteMapProvider.Linq;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using PagedList;

namespace Local_Theatre.Controllers
{
    //only allowing sysAdmin and Mod to have access to this controller
    [Authorize(Roles = "SysAdmin,Moderator")]
    public class AdminController : AccountController
    {

        private LocalTheatreDbContext db = new LocalTheatreDbContext();

        public AdminController() : base() { }

        public AdminController(ApplicationUserManager userManager, ApplicationSignInManager signinManager)
            : base(userManager, signinManager)
        {

        }

        //only allowing SysAdmin to have access to this action
        [Authorize(Roles = "SysAdmin")]
        public ActionResult ViewUsers(string Search, int? page)
        {
            //is search string parameter is null, returning view of users
            if (String.IsNullOrEmpty(Search))
            { 
                return View(db.Users
                    .Include(u => u.Roles)
                    .OrderBy(u => u.FirstName)
                    .ToList()
                    .ToPagedList(page ?? 1, 10));
            }
            //else returning view of users where first or last name contains search string
            return View(db.Users
                .Include(u => u.Roles)
                .Where(u => u.FirstName.Contains(Search.Trim()) || u.LastName.Contains(Search.Trim()))
                .OrderBy(u => u.FirstName)
                .ToList()
                .ToPagedList(page ?? 1, 10));
        }


        [Authorize(Roles = "SysAdmin")]
        [HttpGet]
        public ActionResult CreateStaff()
        {
            //creating new CreateStaffViewModel instance
            CreateStaffViewModel staff = new CreateStaffViewModel();

            //selecting Staff and Mod roles from the database and adding them to selectList
            var roles = db.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Name
            }).Where(b => b.Text.Equals("Staff") || b.Text.Equals("Moderator")).ToList();

            //adding selectlist to staff roles
            staff.Roles = roles;
            //passing the CreateStaffViewModel to the view
            return View(staff);
        }

        [Authorize(Roles = "SysAdmin")]
        [HttpPost]
        public ActionResult CreateStaff(CreateStaffViewModel model)
        {
            if (ModelState.IsValid)
            {
                //receving a CreateStaffViewModel model with the staff details, and creating new staff object
                var newStaff = new Staff
                {
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Addressline1 = model.Addressline1,
                    Addressline2 = model.Addressline2,
                    PostCode = model.PostCode,
                    City = model.City,
                    Email = model.Email,
                    EmailConfirmed = true,
                    PhoneNumber = model.PhoneNumber,
                    PhoneNumberConfirmed = true,
                    Role = model.StaffRole,
                    DateofBirth = model.DateofBirth,
                    RegistereredAt = DateTime.Now
                };
                //creating new user with Usermanager, passing in staff object and model password
                var result = UserManager.Create(newStaff, model.Password);
                if (result.Succeeded)
                {
                    //if result Succeeded, adding staff to role and returing to view
                    UserManager.AddToRole(newStaff.Id, model.Role);
                    return RedirectToAction("ViewUsers", "Admin");
                }
            }
            return View(model);
        }

        [Authorize(Roles = "SysAdmin")]
        [HttpGet]
        public ActionResult EditStaff(string id)
        {
            //checking if recieved id is not null.
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //finding user inside the database as Staff
            var staff = db.Users.Find(id) as Staff;
            //if staff is not null
            if (staff == null)
            {
                return HttpNotFound();
            }
            //creating new EditStaffViewModel and passing in all the required staff details
            return View(new EditStaffViewModel
            {
                FirstName = staff.FirstName,
                LastName = staff.LastName,
                Addressline1 = staff.Addressline1,
                Addressline2 = staff.Addressline2,
                PostCode = staff.PostCode,
                City = staff.City,
                Email = staff.Email,
                EmailConfirmed = staff.EmailConfirmed,
                PhoneNumber = staff.PhoneNumber,
                PhoneConfirmed = staff.PhoneNumberConfirmed,
                DateofBirth = staff.DateofBirth
            });
        }

        [Authorize(Roles = "SysAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditStaff(string id, [Bind(Include = "FirstName, LastName, Addressline1, " +
            "Addressline2, PostCode, City, PhoneNumber, DateofBirth")] EditStaffViewModel model)
        {
            if (ModelState.IsValid)
            {
                //finding staff by id
                var staff = (Staff)await UserManager.FindByIdAsync(id);
                //updating staff model
                UpdateModel(staff);
                var result = await UserManager.UpdateAsync(staff);
                //is sucessfully, return to index viewe
                if (result.Succeeded)
                {
                    return RedirectToAction("ViewUsers", "Admin");
                }
            }
            return View();
        }

        [Authorize(Roles = "SysAdmin")]
        [HttpGet]
        public ActionResult EditMember(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var member = db.Users.Find(id) as Member;
            if (member == null)
            {
                return HttpNotFound();
            }

            return View(new EditMemberViewModel
            {
                FirstName = member.FirstName,
                LastName = member.LastName,
                DateofBirth = member.DateofBirth,
                Email = member.Email,
                EmailConfirmed = member.EmailConfirmed,
                PhoneNumber = member.PhoneNumber,
                PhoneConfirmed = member.PhoneNumberConfirmed
            });
        }

        [Authorize(Roles = "SysAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditMember(string id, [Bind(Include = "FirstName, LastName, DateofBirth, " +
            "Email, EmailConfirmed, PhoneNumber, PhoneNumberConfirmed")] EditMemberViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = (Member)await UserManager.FindByIdAsync(id);
                UpdateModel(customer);
                var result = await UserManager.UpdateAsync(customer);
                if (result.Succeeded)
                {
                    return RedirectToAction("ViewUsers", "Admin");
                }
            }
            return View();
        }

        [Authorize(Roles = "SysAdmin")]
        public ActionResult Details(string id)
        {
            //if is is null, show error message
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //elke, find user by id
            var user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            //if user is staff
            if (user is Staff)
            {
                //redirect to DatailsStaff view
                return View("DatailsStaff", (Staff)user);
            }
            //if user is Member
            if (user is Member)
            {
                //redirect to DatailsCustomer view
                return View("DatailsCustomer", (Member)user);
            }
            //else, show not found error
            return HttpNotFound();
        }


        [Authorize(Roles = "SysAdmin")]
        [HttpGet]
        public async Task<ActionResult> ChangeRole(string id)
        {
            //if is is null, show error message
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //if id == to current userId, return back to view
            if (id == User.Identity.GetUserId())
            {
                return RedirectToAction("ViewUsers", "Admin");
            }
            //find user by id
            User user = await UserManager.FindByIdAsync(id);
            //get the role of the user
            string oldRole = (await UserManager.GetRolesAsync(id)).Single();
            //adding Member, Staff and Mod roles to SelectList
            var item = db.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Name,
                Selected = r.Name == oldRole
            }).Where(b => b.Text.Equals("Member") || b.Text.Equals("Staff") || b.Text.Equals("Moderator")).ToList();
            //returning ChangeRoleViewModel view with the roles and username of a user
            return View(new ChangeRoleViewModel
            {
                UserName = user.UserName,
                Roles = item,
                OldRole = oldRole
            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("ChangeRole")]
        public async Task<ActionResult> ChangeRoleConfirmed(string id, [Bind(Include = "Role")] ChangeRoleViewModel model)
        {
            //if id == to current userId, return back to view
            if (id == User.Identity.GetUserId())
            {
                return RedirectToAction("ViewUsers", "Admin");
            }
            if (ModelState.IsValid)
            {
                User user = await UserManager.FindByIdAsync(id);
                string oldRole = (await UserManager.GetRolesAsync(id)).Single();
                //if old role = to new role, return back to view
                if (oldRole == model.Role)
                {
                    return RedirectToAction("ViewUsers", "Admin");
                }
                //else, remove user from an old role and add to a new one
                await UserManager.RemoveFromRoleAsync(id, oldRole);
                await UserManager.AddToRoleAsync(id, model.Role);

                return RedirectToAction("ViewUsers", "Admin");
            }
            return View(model);
        }

        [Authorize(Roles = "SysAdmin")]
        public async Task<ActionResult> Delete(string id)
        {
            //if is is null, show error message
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //if id == to current userId, return back to view
            if (id == User.Identity.GetUserId())
            {
                return RedirectToAction("ViewUsers", "Admin");
            }
            //else, find user by id
            User user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            //remove all the comments, blogs and likes made by that user before deletign the user himself
            await db.Comments.Where(b => b.UserId == id).ForEachAsync(b => db.Comments.Remove(b));
            await db.Likes.Where(b => b.UserId == id).ForEachAsync(b => db.Likes.Remove(b));
            await db.Blogs.Where(b => b.UserId == id).ForEachAsync(b => db.Blogs.Remove(b));
            db.SaveChanges();
            //remove the user and return back to view
            await UserManager.DeleteAsync(user);
            return RedirectToAction("ViewUsers", "Admin");
        }

        [Authorize(Roles = "SysAdmin")]
        public async Task<ActionResult> SuspendMember(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //find the user by id
            User user = await UserManager.FindByIdAsync(id);
            //if user is in role memeber, change his role to suspended and return back to view
            if (user.CurrentRole.Equals("Member"))
            {
                await UserManager.RemoveFromRoleAsync(id, "Member");
                await UserManager.AddToRoleAsync(id, "Suspended");
            }

            return RedirectToAction("ViewUsers", "Admin");
        }

        [Authorize(Roles = "SysAdmin")]
        public ActionResult ManageMember(int? page) 
        {
            List<Member> list = new List<Member>();
            //for every user in the database, if user is member - add him to list and return the list to the view
            foreach (var m in db.Users) {
                User user = db.Users.Find(m.Id);
                if (user.CurrentRole == "Member") {
                    list.Add((Member)user);
                }
            }
            return View(list.OrderBy(u => u.FirstName).ThenBy(u => u.LastName).ToPagedList(page ?? 1, 5));  
        }

        [Authorize(Roles = "SysAdmin")]
        [HttpGet]
        public async Task<ActionResult> PromoteToRole(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (id == User.Identity.GetUserId())
            {
                return RedirectToAction("ManageMember", "Admin");
            }
            //find user by id
            User user = await UserManager.FindByIdAsync(id);
            //get the role of a user
            string oldRole = (await UserManager.GetRolesAsync(id)).Single();
            //add Staff and Mod roles to SelectList
            var item = db.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Name,
                Selected = r.Name == oldRole
            }).Where(b => b.Text.Equals("Staff") || b.Text.Equals("Moderator")).ToList();
            //pass it to view as ChangeRoleViewModel model
            return View(new ChangeRoleViewModel
            {
                UserName = user.UserName,
                Roles = item,
                OldRole = oldRole
            });
        }

        [Authorize(Roles = "SysAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("PromoteToRole")]
        public async Task<ActionResult> PromoteToRoleConfirmed(string id, [Bind(Include = "Role")] ChangeRoleViewModel model)
        {
            if (id == User.Identity.GetUserId())
            {
                return RedirectToAction("ManageMember", "Admin");
            }
            if (ModelState.IsValid)
            {
                User user = await UserManager.FindByIdAsync(id);
                string oldRole = (await UserManager.GetRolesAsync(id)).Single();
                //if old role = to a new role, return to view
                if (oldRole == model.Role)
                {
                    return RedirectToAction("ManageMember", "Admin");
                }
                //else, remove user from an old role and add to a new one and return back to view
                await UserManager.RemoveFromRoleAsync(id, oldRole);
                await UserManager.AddToRoleAsync(id, model.Role);

                return RedirectToAction("ManageMember", "Admin");
            }
            return View(model);
        }

        public ActionResult ModerateComments(string Search, int? page) 
        {
            //if search string is null, return comments to view
            if (String.IsNullOrEmpty(Search))
            {
                return View(db.Comments
                    .Include(b => b.Blog)
                    .Include(b => b.User)
                    .Where(b => b.CommentApproved == false)
                    .OrderByDescending(b => b.CommentPosted)
                    .ToList()
                    .ToPagedList(page ?? 1, 4));
            }
            //else, return comments where users first or last name contain search string
            return View(db.Comments
                    .Include(b => b.Blog)
                    .Include(b => b.User)
                    .Where(b => b.CommentApproved == false)
                    .Where(b => b.User.FirstName.Contains(Search.Trim()) || b.User.LastName.Contains(Search.Trim()))
                    .OrderByDescending(b => b.CommentPosted)
                    .ToList()
                    .ToPagedList(page ?? 1, 4));
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing) { db.Dispose(); }
            base.Dispose(disposing);
        }
    }
}