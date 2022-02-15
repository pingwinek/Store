using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MVC.Data;
using MVC.Models.IRepository;
using MVC.Models.Repository;
using MVC.Models.Seed;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

builder.Services.AddDbContext<StoreDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<IStoreRepository, StoreRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseDeveloperExceptionPage();
app.UseStatusCodePages();
app.UseStaticFiles();

app.UseRouting();
app.UseEndpoints(endpoints => {
    endpoints.MapControllerRoute(
                        name: "catpage",
                        pattern: "{category}/Page{productPage:int}",
                        new { Controller = "Home", action = "Index" });
    endpoints.MapControllerRoute(
                        "page",
                        "Page{productPage:int}",
                        new { Controller = "Home", action = "Index", productPage = 1 });
    endpoints.MapControllerRoute(
                        "category",
                        "{category}",
                        new { Controller = "Home", action = "Index", productPage = 1 });
    endpoints.MapControllerRoute(
                        "pagination",
                        "Products/Page{productPage}",
                        new { Controller = "Home", action = "Index", productPage = 1 });
    endpoints.MapDefaultControllerRoute();
});

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

SeedData.EnsurePopulated(app);
