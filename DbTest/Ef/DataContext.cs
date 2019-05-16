using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbTest.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DbTest.Ef
{
    public class DataContext :IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PostCategory>()
                .HasKey(bc => new { bc.PostId, bc.CategoryId });
            modelBuilder.Entity<PostCategory>()
                .HasOne(bc => bc.Post)
                .WithMany(b => b.PostCategory)
                .HasForeignKey(bc => bc.PostId);
            modelBuilder.Entity<PostCategory>()
                .HasOne(bc => bc.Category)
                .WithMany(c => c.PostCategory)
                .HasForeignKey(bc => bc.CategoryId);
        }

        public DbSet<DbTest.Models.PostCategory> PostCategory { get; set; }

        
    }
}
