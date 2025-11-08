using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tradio.Domain;

namespace Tradio.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUserChat>()
                .HasKey(auc => new {auc.ApplicationUserId, auc.ChatId});

            builder.Entity<ApplicationUserChat>()
                .HasOne<ApplicationUser>()
                .WithMany(au => au.ApplicationUserChats)
                .HasForeignKey(au => au.ApplicationUserId);

            builder.Entity<Payment>()
                .HasOne<ApplicationUser>()
                .WithMany(au => au.Payments)
                .HasForeignKey(p => p.ApplicationUserId);

            builder.Entity<Complaint>()
                .HasOne<ApplicationUser>()
                .WithMany(au => au.Complaints)
                .HasForeignKey(c => c.ApplicationUserId);

            builder.Entity<ComplaintReply>()
                .HasOne<ApplicationUser>()
                .WithMany(au => au.ComplaintReplies)
                .HasForeignKey(p => p.ApplicationUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Service> Services { get; set; }

        public DbSet<ApplicationUserChat> ApplicationUserChats { get; set; }

        public DbSet<Chat> Chats { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Country> Countres { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<SubscriptionType> SubscriptionTypies { get; set; }

        public DbSet<Complaint> Complaints { get; set; }

        public DbSet<ComplaintStatus> ComplaintStatuses { get; set; }

        public DbSet<ComplaintReply> ComplaintReplies { get; set; }
    }
}
