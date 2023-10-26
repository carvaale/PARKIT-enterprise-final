using Microsoft.EntityFrameworkCore;
using PARKIT_enterprise_final.Models;

namespace PARKIT_enterprise_final.Repository
{
    public partial class ListingDBContext : DbContext
    {
        public ListingDBContext(DbContextOptions<ListingDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Listing> Listings { get; set; }
    }
}
