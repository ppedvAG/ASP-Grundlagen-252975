using M001;

public class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.AddControllersWithViews();

		//Dependency Injection
		//Über die Add Methoden können Klassen zum Dependency Injection Container hinzugefügt werden
		//Im Konstruktor der Controller werden die Objekte dann empfangen
		builder.Services.AddRateLimiter(); //Kurzform für Rate Limiting

		//builder.Services.AddMetrics(); //Langform
		//builder.Services.AddSingleton<RateLimiter>(); //Langform

		//Eigene Klasse per DI einbinden
		builder.Services.AddSingleton<DITest>();

		//Mit eigener Erweiterungsmethode
		builder.Services.AddDITest();

		//Erweiterungsmethoden
		int x = 13954;
		x.Quersumme();

		WebApplication app = builder.Build(); //Hier wird DI abgeschlossen

		//////////////////////////////////////////////////////////////////////////

		//Middleware
		//HTTP-Request Pipeline
		//Jeder Request geht die folgenden Schritte durch
		//Kann auch eigene Komponenten enthalten

		// Configure the HTTP request pipeline.
		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Home/Error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
		}

		app.UseHttpsRedirection();
		app.UseRouting();

		//Eigene Middleware Komponente registrieren
		app.UseMiddleware<TestMiddleware>();

		app.UseAuthorization();

		app.MapStaticAssets();

		app.MapControllerRoute(
			name: "default",
			pattern: "{controller=Home}/{action=Index}/{id?}")
			.WithStaticAssets();

		app.Run();
	}
}

/// <summary>
/// Erweiterungsmethoden
/// 
/// Methoden, welche an beliebige Typen angehängt werden können
/// Müssen in einer statischen Klasse und selbst als statisch definiert sein
/// </summary>
public static class ExtensionMethods
{
	public static int Quersumme(this int x)
	{
		return (int) x.ToString().Sum(char.GetNumericValue);
	}

	public static IServiceCollection AddDITest(this IServiceCollection services)
	{
		services.AddSingleton<DITest>();
		return services;
	}
}

/// <summary>
/// Startup.cs
/// 
/// Legt DI und Middleware in eigene Methoden
/// </summary>
public class Startup
{
	public static void ConfigureServices(IServiceCollection services)
	{
		services.AddControllersWithViews();

		//Dependency Injection
		//Über die Add Methoden können Klassen zum Dependency Injection Container hinzugefügt werden
		//Im Konstruktor der Controller werden die Objekte dann empfangen
		services.AddRateLimiter(); //Kurzform für Rate Limiting

		//builder.Services.AddMetrics(); //Langform
		//builder.Services.AddSingleton<RateLimiter>(); //Langform

		//Eigene Klasse per DI einbinden
		services.AddSingleton<DITest>();

		//Mit eigener Erweiterungsmethode
		services.AddDITest();

		//Erweiterungsmethoden
		int x = 13954;
		x.Quersumme();
	}

	public static void ConfigureMiddleware(WebApplication app)
	{
		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Home/Error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
		}

		app.UseHttpsRedirection();
		app.UseRouting();

		//Eigene Middleware Komponente registrieren
		app.UseMiddleware<TestMiddleware>();

		app.UseAuthorization();

		app.MapStaticAssets();

		app.MapControllerRoute(
			name: "default",
			pattern: "{controller=Home}/{action=Index}/{id?}")
			.WithStaticAssets();

		app.Run();
	}
}