using MakeenCo_Work.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeenCo_Work.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


        public DbSet<User>users { get; set; }
        public DbSet<Role>roles { get; set; }
        public DbSet<FAQ>fAQs { get; set; }
        public DbSet<BlogPost>blogPosts { get; set; }
        public DbSet<Content> content { get; set; }
        public DbSet<DiscountCode> discountCodes { get; set; }
        public DbSet<Regulation> regulation { get; set; }   
        public DbSet<Reservation> reservation { get; set; }
        




    }
}
