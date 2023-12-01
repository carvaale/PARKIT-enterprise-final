using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PARKIT_enterprise_final.Areas.Identity.Data;
using PARKIT_enterprise_final.Models;

namespace PARKIT_enterprise_final.Areas.Identity.Data;

//public class ApplicationContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
public class ApplicationContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    protected ApplicationContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.Entity<ApplicationUser>()
            .HasOne(u => u.User)
            .WithOne()
            .HasForeignKey<User>(u => u.ApplicationUserId);
    }
}
