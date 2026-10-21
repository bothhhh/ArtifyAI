using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ArtifyAI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Create Admin role and assign it to an existing account
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider
        .GetRequiredService<RoleManager<IdentityRole>>();

    var userManager = scope.ServiceProvider
        .GetRequiredService<UserManager<IdentityUser>>();

    // Create Admin role if it does not exist
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        var roleResult = await roleManager.CreateAsync(
            new IdentityRole("Admin"));

        if (!roleResult.Succeeded)
        {
            throw new InvalidOperationException(
                "Failed to create the Admin role.");
        }
    }

    // Assign Admin role to your existing account
    var adminEmail = builder.Configuration["AdminSeed:Email"];

    if (!string.IsNullOrWhiteSpace(adminEmail))
    {
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            throw new InvalidOperationException(
                "The specified Admin account does not exist. Register it first.");
        }

        if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
        {
            var result = await userManager.AddToRoleAsync(
                adminUser, "Admin");

            if (!result.Succeeded)
            {
                throw new InvalidOperationException(
                    "Failed to assign the Admin role.");
            }
        }
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
