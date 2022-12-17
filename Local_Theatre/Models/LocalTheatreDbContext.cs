using System.Data.Entity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Local_Theatre.Models
{
    public class LocalTheatreDbContext : IdentityDbContext<User>
    {
        //DbSets of Lists classes
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }

        //creating commenction to the datbase and seeding
        public LocalTheatreDbContext() : base("LocalTheatreConnection", throwIfV1Schema: false) {
            Database.SetInitializer(new DatabaseInitialiser());
        }

        public static LocalTheatreDbContext Create()
        {
            return new LocalTheatreDbContext();
        }
    }
}