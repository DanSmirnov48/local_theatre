using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Local_Theatre.Models
{
    //Staff class inheriting from User class
    public class Staff : User
    {
        //---------------------------------------
        [Display(Name = "Address Line 1")]
        public string Addressline1 { get; set; }

        //---------------------------------------
        [Display(Name = "Address Line 2")]
        public string Addressline2 { get; set; }

        //---------------------------------------
        [Display(Name = "Post Codee")]
        public string PostCode { get; set; }

        //---------------------------------------
        public string City { get; set; }

        //---------------------------------------
        public StaffRole Role { get; set; }


        //-----------------------Navigational propery to Blog class-------------------------------------------
        public Staff() : base()
        {
            Blogs = new List<Blog>();
        }
        public virtual ICollection<Blog> Blogs { get; set; }


    }
    public enum StaffRole
    {
        STAFF,
        MODERATOR,
        SYSADMIN
    }
}