using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TastyDelivery.Infrastructure.Data;

public class TastyDeliveryDbContext : IdentityDbContext<IdentityUser>
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
