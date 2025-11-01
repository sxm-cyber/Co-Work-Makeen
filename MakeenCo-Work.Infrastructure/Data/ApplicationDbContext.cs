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

        public DbSet<Image> Images { get; set; }

        public DbSet<WhyMakeen>WhyMakeens { get; set; }

        public DbSet<MoreThanAcademy>MoreThanAcademies { get; set; }
       
        public DbSet<MainBaner> MainBaners { get; set; }

        public DbSet<WaysOfCommunication>WaysOfCommunications { get; set; }

        public DbSet<Message>Messages { get; set; }




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
            builder.Entity<Image>().ToTable("Image");
            builder.Entity<WhyMakeen>().ToTable("WhyMakeen");
            builder.Entity<MoreThanAcademy>().ToTable("MoreThanAcademy");
            builder.Entity<WaysOfCommunication>().ToTable("WaysOfCommunication");
            builder.Entity<MainBaner>().ToTable("MainBaner");
            builder.Entity<Message>().ToTable("Message");


            builder.Entity<Message>()
        .HasOne(m => m.Sender)
        .WithMany()  // فعلاً در User لیست پیام‌ها نداریم
        .HasForeignKey(m => m.SenderId)
        .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
                .HasOne(m => m.Recipient)
                .WithMany()
                .HasForeignKey(m => m.RecipientId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
