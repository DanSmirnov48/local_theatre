using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Local_Theatre.Models.ViewModels
{
    //CreateStaffViewModel class to create new Staff object
    public class CreateStaffViewModel
    {
        //---------------------------------------
        [Display(Name = "First Name")]
        public string FirstName { get; set; }


        //---------------------------------------
        [Display(Name = "LastName")]
        public string LastName { get; set; }


        //---------------------------------------
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Date of Birth")]
        public DateTime DateofBirth { get; set; }


        //---------------------------------------
        [Display(Name = "Address Line 1")]
        public string Addressline1 { get; set; }


        //---------------------------------------
        [Display(Name = "Address Line 2")]
        public string Addressline2 { get; set; }


        //---------------------------------------
        [DataType(DataType.PostalCode)]
        [Display(Name = "Post Codee")]
        public string PostCode { get; set; }


        //---------------------------------------
        public string City { get; set; }


        //---------------------------------------
        [Display(Name = "Email Address")]
        public string Email { get; set; }


        //---------------------------------------
        [Display(Name = "Email Confirmed")]
        public bool EmailConfirmed { get; set; }


        //---------------------------------------
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }


        //---------------------------------------
        [Display(Name = "Phone Confirmed")]
        public bool PhoneConfirmed{ get; set; }


        //---------------------------------------
        public string Password { get; set; }


        //---------------------------------------
        public StaffRole StaffRole { get; set; }

        //---------------------------------------
        public string Role { get; set; }
        public ICollection<SelectListItem> Roles { get; set; }

    }
}