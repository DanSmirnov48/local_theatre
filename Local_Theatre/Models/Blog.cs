using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Local_Theatre.Models
{
    public class Blog
    {
        [Key]
        public int BlogId { get; set; }

        //---------------------------------------
        [Required]
        [Display(Name = "Blog Title")]
        public string BlogTitle { get; set; }

        //---------------------------------------
        [Required]
        [Display(Name = "Blog Context")]
        [DataType(DataType.MultilineText)]
        public string BlogContext { get; set; }

        //---------------------------------------
        [Display(Name = "Blog Post Date")]
        [DisplayFormat(DataFormatString = "{dd/MMM/yyyy}")]
        public DateTime BlogPosted{ get; set; }

        [Display(Name = "Blog Approved")]
        public bool BlogApproved { get; set; }

        //-----------------Navigational propery to Staff class-------------------------------------------------
        [ForeignKey("Staff")]
        [Display(Name = "Blog By")]
        public string UserId { get; set; }
        public Staff Staff { get; set; }

        //------------------Navigational propery to Category class------------------------------------------------
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }


        //------------------------------------------------------------------
        public Blog()
        {
            Comments = new List<Comment>();
            Likes = new List<Like>();
        }
        //list of Comments and Likes
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }

    }
}