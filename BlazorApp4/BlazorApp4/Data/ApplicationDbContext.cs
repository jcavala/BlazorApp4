using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace BlazorApp4.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {     
      public DbSet<Discount> Discounts {  get; set; }
      public DbSet<Product> Products { get; set; }
      public DbSet<Store> Stores { get; set; }
    }
}
