using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Puskemas.Data;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------
// DATABASE CONFIGURATION
// ----------------------------
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                      ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// ----------------------------
// IDENTITY CONFIGURATION (Jika diperlukan login/akun)
// ----------------------------
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // ubah ke true jika perlu konfirmasi email
})
.AddEntityFrameworkStores<AppDbContext>();

// ----------------------------
// MVC + Razor Pages
// ----------------------------
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// ----------------------------
// MIDDLEWARE
// ----------------------------
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Penting jika menggunakan Identity
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // Aktifkan Razor Pages untuk Identity

app.Run();
