using AppDev.Models;
using AppDev.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Data
{
  public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Todo> Todoes { get; set; }
    public DbSet<Category> Categories { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
      this.SeedRoles(builder);
    }

    private void SeedRoles(ModelBuilder builder)
    {
      builder.Entity<IdentityRole>().HasData(
          new IdentityRole() { Id = "fab4fac1-c546-41de-aebc-a14da6895711", Name = Role.USER, ConcurrencyStamp = "1", NormalizedName = Role.USER },
          new IdentityRole() { Id = "c7b013f0-5201-4317-abd8-c211f91b7330", Name = Role.ADMIN, ConcurrencyStamp = "2", NormalizedName = Role.ADMIN }
          );
    }
  }
}

