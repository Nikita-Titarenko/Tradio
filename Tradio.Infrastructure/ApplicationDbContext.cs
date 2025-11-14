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

            builder.Entity<ApplicationUser>()
                .HasOne(u => u.City)
                .WithMany()
                .HasForeignKey(au => au.CityId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Service>()
                .HasOne<ApplicationUser>()
                .WithMany(au => au.Services)
                .HasForeignKey(au => au.ApplicationUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ApplicationUserService>()
                .HasOne<ApplicationUser>()
                .WithMany(au => au.ApplicationUserServices)
                .HasForeignKey(au => au.ApplicationUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<UserSubscription>()
                .HasOne<ApplicationUser>()
                .WithMany(au => au.UserSubscriptions)
                .HasForeignKey(p => p.ApplicationUserId);

            builder.Entity<ComplaintReply>()
                .HasOne<ApplicationUser>()
                .WithMany(au => au.ComplaintReplies)
                .HasForeignKey(p => p.ApplicationUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Service> Services { get; set; }

        public DbSet<ApplicationUserService> ApplicationUserServices { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Country> Countres { get; set; }

        public DbSet<UserSubscription> UserSubscriptions { get; set; }

        public DbSet<SubscriptionType> SubscriptionTypies { get; set; }

        public DbSet<Complaint> Complaints { get; set; }

        public DbSet<ComplaintReply> ComplaintReplies { get; set; }
    }
}
