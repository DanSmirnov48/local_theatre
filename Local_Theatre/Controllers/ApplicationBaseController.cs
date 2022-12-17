using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Local_Theatre.Models;

namespace Local_Theatre.Controllers
{
    public class ApplicationBaseController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (User != null)
            {
                var username = User.Identity.Name;

                if (!string.IsNullOrEmpty(username))
                {
                    var user = new LocalTheatreDbContext().Users.SingleOrDefault(u => u.UserName == username);
                    //string fullName = string.Concat(new string[] { user.FirstName, " ", user.LastName });
                    ViewData.Add("FullName", user.FirstName);
                }
            }
            base.OnActionExecuted(filterContext);
        }
        public ApplicationBaseController()
        { }
    }
}