using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Local_Theatre.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        //---------------------------------------
        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Comment Context")]
        public string CommentContext{ get; set; }

        //---------------------------------------
        [Display(Name = "Comment Post Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime CommentPosted { get; set; }

        //---------------------------------------
        
        [ForeignKey("User")]
        [Display(Name = "Comment By")]
        public string UserId { get; set; }
        public User User { get; set; }

        //---------------------------------------
        [Display(Name = "Comment Approved")]
        public bool CommentApproved{ get; set; }


        //-------------------Navigational propery to Blog class-----------------------------------------------
        [ForeignKey("Blog")]
        public int BlogId { get; set; }
        public Blog Blog { get; set; }

    }
}