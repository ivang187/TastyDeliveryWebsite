using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using TastyDelivery.Infrastructure.Data;
using TastyDelivery.Infrastructure.Data.Models.Enums;
using TastyDelivery.Infrastructure.Data.Models.IdentityModels;

namespace TastyDelivery.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder SeedAdmin(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var services = scopedServices.ServiceProvider;

            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var dbContext = services.GetRequiredService<TastyDeliveryDbContext>();

            Task.Run(async () =>
            {
                var adminUser = await userManager.FindByEmailAsync("ivang187@gmail.com");

                if(adminUser.Role == UserRole.Admin)
                {
                    return;
                }

                var getRoles = await userManager.GetRolesAsync(adminUser);
                var removeRoles = await userManager.RemoveFromRolesAsync(adminUser, getRoles);
                var addToRoleResult = await userManager.AddToRoleAsync(adminUser, UserRole.Admin.ToString());

                if (addToRoleResult.Succeeded)
                {
                    adminUser.Role = UserRole.Admin;
                    await dbContext.SaveChangesAsync(); 
                }
                else
                {
                    // Handle failed role assignment
                    // Log error or throw exception
                }


            }).GetAwaiter().GetResult();

            return app;
        }

        public static IApplicationBuilder SeedRolesFromEnum(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();
            var services = scopedServices.ServiceProvider;

            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task.Run(async () =>
            {
                foreach (var roleName in Enum.GetNames(typeof(UserRole)))
                {
                    if (await roleManager.RoleExistsAsync(roleName))
                    {
                        continue;
                    }

                    var role = new IdentityRole { Name = roleName };
                    await roleManager.CreateAsync(role);
                }
            }).GetAwaiter().GetResult();

            return app;
        }

    }
}
