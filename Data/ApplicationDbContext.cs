using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DragonBlog2.Models;

namespace DragonBlog2.Data
{
    public class ApplicationDbContext : IdentityDbContext<BlogUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<DragonBlog2.Models.Blog> Blog { get; set; }
        public DbSet<DragonBlog2.Models.Comment> Comment { get; set; }
        public DbSet<DragonBlog2.Models.Post> Post { get; set; }
        public DbSet<DragonBlog2.Models.Tag> Tag { get; set; }
    }
}
