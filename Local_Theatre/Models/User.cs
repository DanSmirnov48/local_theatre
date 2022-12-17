using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace Local_Theatre.Models
{
    //User class inheriting from IdentityUser class
    public abstract class User : IdentityUser
    {
        //---------------------------------------
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        //---------------------------------------
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        //---------------------------------------

        [Display(Name = "Date of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime DateofBirth { get; set; }

        //---------------------------------------
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        [Display(Name = "Joined")]
        public DateTime RegistereredAt { get; set; }


        //----------------List of Comments and Likes-----------------------
        public User()
        {
            Comments = new List<Comment>();
            Likes = new List<Like>();
        }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }

        //---------------------------------------
        private ApplicationUserManager userManager;

        [NotMapped]
        [Display(Name = "Role")]
        public string CurrentRole
        {
            get
            {
                if (userManager == null)
                {
                    userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
                }
                return userManager.GetRoles(Id).Single();
            }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }

}