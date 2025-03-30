using FinanceTrackingWebAPI.Authentication;
using FinanceTrackingWebAPI.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace FinanceTrackingWebAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserExpenses>().HasOne(e => e.Expenses).WithMany(ue => ue.userExpenses)
                .HasForeignKey(e => e.ExpenseId).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserExpenses>().HasOne(e => e.ApplicationUser).WithMany(ue => ue.userExpenses)
                .HasForeignKey(e => e.UserId);

            builder.Entity<UsersGroup>().HasOne(e => e.Groups).WithMany(ue => ue.usersGroup)
            .HasForeignKey(e => e.GroupId).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UsersGroup>().HasOne(e => e.ApplicationUser).WithMany(ue => ue.usersGroup)
                .HasForeignKey(e => e.UserId);

            builder.Entity<ApplicationUser>().HasMany(f => f.Friend).WithOne(u => u.ApplicationUser).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Expense>()
            .HasOne(b => b.Groups)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
        }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<UserExpenses> UserExpenses { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UsersGroup> UsersGroup { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<FriendRequest> FriendRequests { get; set; }

    }
}
