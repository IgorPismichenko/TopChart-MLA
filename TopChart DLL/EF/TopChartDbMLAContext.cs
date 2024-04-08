using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TopChart_DLL.Entities;

namespace TopChart_DLL.EF
{
    public class TopChartDbMLAContext : DbContext
    {
        public TopChartDbMLAContext(DbContextOptions<TopChartDbMLAContext> options)
                : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Tracks> Tracks { get; set; }

        public DbSet<Singer> Singer { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Video> Video { get; set; }

        public DbSet<Comment> Comment { get; set; }
        public DbSet<CommentVideo> CommentVideo { get; set; }
    }
}
