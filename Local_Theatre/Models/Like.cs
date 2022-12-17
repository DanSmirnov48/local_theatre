using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Local_Theatre.Models
{
    public class Like
    {
        [Key]
        public int LikeId { get; set; }

        //--------------------Navigational propery to User class----------------------------------------------
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }

        //---------------------Navigational propery to Blog class---------------------------------------------
        [ForeignKey("Blog")]
        public int BlogId { get; set; }
        public Blog Blog{ get; set; }
    }
}