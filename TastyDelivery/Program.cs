using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TastyDelivery.Core.Services.Extensions;
using TastyDelivery.Extensions;
using TastyDelivery.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationDbContext(builder.Configuration);
builder.Services.AddApplicationIdentity(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapDefaultControllerRoute();
app.MapRazorPages();

app.SeedRolesFromEnum();
app.UseSession();

await app.RunAsync();
