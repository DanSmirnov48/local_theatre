using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Local_Theatre.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        //---------------------------------------
        [Required]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }


        //-------------------Navigational propery to Blog class-----------------------------------------------
        public Category()
        {
            Blogs = new List<Blog>();
        }
        public virtual ICollection<Blog> Blogs { get; set; }
    }
}