using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PARKIT_enterprise_final.Areas.Identity.Data;
using PARKIT_enterprise_final.Models;
using System.Reflection.Emit;

namespace PARKIT_enterprise_final.Models.DBContext
{
    public class PARKITDBContext : ApplicationContext
    {
        public PARKITDBContext(DbContextOptions<PARKITDBContext> options) : base(options)
        {
        }

        public DbSet<Listing> Listings { get; set; }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<User> UserInfos { get; set; }

        // Didnt add to onmodelCreating function
        public DbSet<Address> Addresses { get; set; }

        // Didnt add to onmodelCreating function
        public DbSet<Wallet> Wallets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin<Guid>>().HasNoKey();
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasNoKey();
            modelBuilder.Entity<IdentityUserToken<Guid>>().HasNoKey();
            modelBuilder.Entity<Listing>().OwnsOne(l => l.Address);
            modelBuilder.Entity<Listing>().HasKey(l => l.Id);
            modelBuilder.Entity<Listing>().OwnsMany(l => l.Images);


            modelBuilder.Entity<User>().OwnsOne(u => u.Address);
            modelBuilder.Entity<User>().HasOne(u => u.Wallet).WithOne().HasForeignKey<Wallet>();
            //modelBuilder.Entity<User>()
            //    .HasOne(u => u.Wallet)
            //    .WithOne()
            //    .HasForeignKey<Wallet>(w=>w.OwnerId);
            modelBuilder.Entity<User>().HasMany(u => u.Listings).WithOne(l => l.User);

            // test




        }
    }
}
