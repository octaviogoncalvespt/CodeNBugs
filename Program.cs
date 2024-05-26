using Microsoft.Extensions.Azure;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Repo.Services;
using Microsoft.AspNetCore.Http;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using DataHub.Models;
using PlantEShop.Controllers.Cart;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUploadImageService, UploadImageService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped<IForumService, ForumService>();
builder.Services.AddScoped<IChallengeService, ChallengeService>();

// Identity Config
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
});

// DbContext Configuration
var connectionString = builder.Configuration.GetConnectionString("DbConnectionString")
                      ?? throw new InvalidOperationException("Connection string 'DbConnectionString' not found.");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddAzureClients(clientBuilder =>
{
    clientBuilder.AddBlobServiceClient(builder.Configuration["AzureBlobStorageConnection"]);
    clientBuilder.AddQueueServiceClient(builder.Configuration["AzureBlobStorageConnection"]);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

AppDbInitializer.SeedUsersAndRolesAsync(app).Wait();

app.Run();
