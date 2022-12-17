using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Local_Theatre.Models
{
    //Member class inheriting from User class
    public class Member : User
    {
        //---------------------------------------
        public MemberStatus MemberStatus { get; set; }

        public Membership Membership { get; set; }
    }

    public enum MemberStatus
    {
        SUSPENDED,
        ACTIVE
    }

    public enum Membership
    {
        FREE_MEMBERSHIP,
        PAID_MEMBERSHIP
    }

}