using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using TastyDelivery.Infrastructure.Data.Models;
using TastyDelivery.Infrastructure.Data.Models.IdentityModels;
using TastyDelivery.Infrastructure.Data.SeedData;

namespace TastyDelivery.Infrastructure.Data;

public class TastyDeliveryDbContext : IdentityDbContext<ApplicationUser>
{
    public TastyDeliveryDbContext(DbContextOptions<TastyDeliveryDbContext> options)
        : base(options)
    {
    }

    public DbSet<Restaurant> Restaurants { get; set; } = null!;

    public DbSet<Product> Products { get; set; } = null!;

    public DbSet<ProductsRestaurants> ProductsRestaurants { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ProductsRestaurants>()
            .HasKey(pr => new { pr.RestaurantId, pr.ProductId });

        builder.ApplyConfiguration(new RestaurantConfiguration());
        builder.ApplyConfiguration(new ProductConfiguration());
        builder.ApplyConfiguration(new ProductRestaurantsConfiguration());
        base.OnModelCreating(builder);
    }
}
