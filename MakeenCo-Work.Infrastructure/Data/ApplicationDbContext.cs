using MakeenCo_Work.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MakeenCo_Work.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<FAQ> Faqs { get; set; }

        public DbSet<BlogPost> BlogPosts { get; set; }

        public DbSet<Content> Contents { get; set; }

        public DbSet<DiscountCode> DiscountCodes { get; set; }

        public DbSet<Regulation> Regulations { get; set; }
        
        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Space> Spaces { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable("Users");
            builder.Entity<Role>().ToTable("Roles");
            builder.Entity<FAQ>().ToTable("Faqs");
            builder.Entity<BlogPost>().ToTable("BlogPosts");
            builder.Entity<Content>().ToTable("Contents");
            builder.Entity<DiscountCode>().ToTable("DiscountCodes");
            builder.Entity<Regulation>().ToTable("Regulations");
            builder.Entity<Reservation>().ToTable("Reservations");
            builder.Entity<Space>().ToTable("Spaces");
        }
    }
}
