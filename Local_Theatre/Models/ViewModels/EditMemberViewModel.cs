using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Local_Theatre.Models.ViewModels
{
    //EditMemberViewModel to Edit Member properties
    public class EditMemberViewModel
    {
        //---------------------------------------
        [Display(Name = "First Name")]
        public string FirstName { get; set; }


        //---------------------------------------
        [Display(Name = "LastName")]
        public string LastName { get; set; }


        //---------------------------------------
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{dd/MMM/yyyy}")]
        [Display(Name = "Date of Birth")]
        public DateTime DateofBirth { get; set; }


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
        public bool PhoneConfirmed { get; set; }

    }
}