using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbTest.Models;
using Microsoft.EntityFrameworkCore;

namespace DbTest.Ef
{
    public class DataContext : DbContext
    {
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Post> Post { get; set; }
    }  
}
