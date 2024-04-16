using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TastyDelivery.Core.Contracts;
using TastyDelivery.Core.Services.Common;
using TastyDelivery.Infrastructure.Data;
using TastyDelivery.Infrastructure.Data.Models.IdentityModels;
using Repository = TastyDelivery.Core.Services.Common.Repository;

namespace TastyDelivery.Core.Services.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;
            });
            services.AddScoped<IRestaurantService, RestaurantService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<IAdminService, AdminService>();  
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IDeliveryManService, DeliveryManService>();

            return services;
        }

        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string not found");

            services.AddDbContext<TastyDeliveryDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddTransient<IRepository, Repository>();

            return services;
        }

        public static IServiceCollection AddApplicationIdentity(this IServiceCollection services, IConfiguration config)
        {
            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<TastyDeliveryDbContext>();

            return services;
        }
    }
}

