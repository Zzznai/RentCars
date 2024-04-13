using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentCars.Data;
using RentCars.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure DbContext to use SQL Server with connection string from appsettings.json
builder.Services.AddDbContext<RentCarDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity with RentCarUser as user type and IdentityRole as role type, and use Entity Framework for storage
builder.Services.AddIdentity<RentCarUser, IdentityRole>().AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<RentCarDbContext>();

// Configure session options
builder.Services.AddSession(options =>
{
    // Set session timeout to 60 minutes
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    // Set session cookie properties
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Seed admin user data when the application starts
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await AdminUserSeeder.Initialize(services);
    await AdminUserSeeder.AddAdminUser(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // Handle errors in production environment
    app.UseExceptionHandler("/Home/Error");
    // Enable HTTPS redirection and HSTS (HTTP Strict Transport Security)
    app.UseHsts();
}

// Enable HTTPS redirection
app.UseHttpsRedirection();
// Serve static files (HTML, CSS, JavaScript, images, etc.)
app.UseStaticFiles();

// Enable routing
app.UseRouting();

// Enable authentication and authorization
app.UseAuthorization();

// Configure endpoints
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Enable session support
app.UseSession();

// Run the application
app.Run();
