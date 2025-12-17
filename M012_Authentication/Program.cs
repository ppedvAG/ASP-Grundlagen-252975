using M012_Authentication.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//////////////////////////////////////////////////////////////////////////////

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
	options.SignIn.RequireConfirmedAccount = true;

	options.Password.RequireDigit = false;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequiredLength = 3;
	options.Password.RequireLowercase = false;
	options.Password.RequireUppercase = false;
})
	.AddRoles<IdentityRole>() //Das Role-System hinzuf�gen
	.AddEntityFrameworkStores<ApplicationDbContext>();

//SignInManager: Allgemeine Userverwaltung (Einloggen, Ausloggen, ...)

//UserManager: Gibt Informationen �ber den derzeitigen User

//RoleManager: Verwaltung von Rollen

//////////////////////////////////////////////////////////////////////////////

builder.Services.AddControllersWithViews();

var app = builder.Build();

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
