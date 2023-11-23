using Microsoft.EntityFrameworkCore;
using PARKIT_enterprise_final.Models;

namespace PARKIT_enterprise_final.Models.DBContext
{
    public class PARKITDBContext : DbContext
    {
        public PARKITDBContext(DbContextOptions<PARKITDBContext> options) : base(options)
        {
        }

        public DbSet<Listing> Listings { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<User> Users { get; set; }

        // Didnt add to onmodelCreating function
        public DbSet<Address> Addresses { get; set; }

        // Didnt add to onmodelCreating function
        public DbSet<Wallet> Wallets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Listing>().OwnsOne(l => l.Address);
            modelBuilder.Entity<Listing>().OwnsMany(l => l.Images);
            modelBuilder.Entity<User>().OwnsOne(u => u.Address);
            modelBuilder.Entity<User>().OwnsOne(u => u.Wallet);
            modelBuilder.Entity<User>().HasMany(u => u.Listings).WithOne(l => l.User);


        }
    }
}
