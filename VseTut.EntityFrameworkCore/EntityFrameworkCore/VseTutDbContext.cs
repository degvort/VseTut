using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VseTut.Core.Categories.Model;
using VseTut.Core.SubCategories.Model;
using VseTut.Core.Users.Model;

namespace VseTut.EntityFrameworkCore.EntityFrameworkCore
{
    public class VseTutDbContext : IdentityDbContext<User>
    {
        public DbSet<Category> Categories { get; set; }
        
        public DbSet<SubCategory> SubCategories { get; set; }

        public VseTutDbContext(DbContextOptions<VseTutDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
