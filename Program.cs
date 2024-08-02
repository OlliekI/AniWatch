using AniWatch.Data;
using AniWatch.Data.Enums;
using AniWatch.Data.Services;
using AniWatch.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));




builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
/*builder.Services.AddRouting(options => options.LowercaseUrls = true);
*/
builder.Services.AddScoped<ICharactersService, CharactersService>();
builder.Services.AddScoped<IProducersService, ProducersService>();
builder.Services.AddScoped<IAnimeService, AnimeService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddControllersWithViews();





builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


AppDbInitializer.Seed(app);


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "Anime",
    pattern: "{controller=Animes}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "AdminArea",
    pattern: "{areas=exists}/{controller=Home}/{action=Index}/{id?}");



using (var scope = app.Services.CreateScope())
{
    var roleManager =
        scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "Manager" };
    foreach (var role in roles)
    {
        if(!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));  
    }

}

app.Run();
