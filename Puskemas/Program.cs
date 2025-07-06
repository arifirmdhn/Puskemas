using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Puskemas.Data;

var builder = WebApplication.CreateBuilder(args);

// Ambil connection string dari appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Tambahkan ApplicationDbContext (untuk Identity)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Tambahkan AppDbContext (jika kamu punya context untuk data lain)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Konfigurasi Identity + Roles
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddRoles<IdentityRole>() // ?? Tambahkan ini agar bisa menggunakan Role
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Exception page saat development
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Authorization
builder.Services.AddAuthorization();

// Razor Pages dan MVC Controller
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ?? Panggil seeding Admin setelah app.Build()
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await CreateAdminAsync(services);
}

// Error handling
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

app.UseAuthentication();
app.UseAuthorization();

// Konfigurasi route utama
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();


// ?? METHOD SEEDING ADMIN
async Task CreateAdminAsync(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

    string roleName = "Admin";
    string adminEmail = "admin@email.com";
    string adminPassword = "Admin123!";

    // Buat role Admin jika belum ada
    if (!await roleManager.RoleExistsAsync(roleName))
    {
        await roleManager.CreateAsync(new IdentityRole(roleName));
    }

    // Buat user Admin jika belum ada
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        var user = new IdentityUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(user, adminPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, roleName);
        }
    }
}
