using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
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

            Task.Run(async () =>
            {
                if (await roleManager.RoleExistsAsync("Admin"))
                {
                    return;
                }
                

                var role = new IdentityRole { Name = "Admin" };

                await roleManager.CreateAsync(role);

                var admin = await userManager.FindByNameAsync("ivang187@gmail.com");

                await userManager.AddToRoleAsync(admin, role.Name);
            }).GetAwaiter()
            .GetResult();
            
            return app;              
        }
    
    }
}
