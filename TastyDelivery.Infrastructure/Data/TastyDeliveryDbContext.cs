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

    public DbSet<Order> Orders { get; set; } = null!;

    public DbSet<OrderProducts> OrderProducts { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ProductsRestaurants>()
            .HasKey(pr => new { pr.RestaurantId, pr.ProductId });

        builder.Entity<OrderProducts>()
            .HasKey(pr => new { pr.OrderId, pr.ProductId });

        builder.Entity<Restaurant>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

        builder.Entity<Order>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

        builder.Entity<Product>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

        builder.ApplyConfiguration(new RestaurantConfiguration());
        builder.ApplyConfiguration(new ProductConfiguration());
        builder.ApplyConfiguration(new ProductRestaurantsConfiguration());
        base.OnModelCreating(builder);
    }
}
