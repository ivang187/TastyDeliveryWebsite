using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TastyDelivery.Infrastructure.Data.Models.IdentityModels;

namespace TastyDelivery.Infrastructure.Data;

public class TastyDeliveryDbContext : IdentityDbContext<ApplicationUser>
{
    public TastyDeliveryDbContext(DbContextOptions<TastyDeliveryDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
