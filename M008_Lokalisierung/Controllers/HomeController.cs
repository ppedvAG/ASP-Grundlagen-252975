using M008_Lokalisierung.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Diagnostics;
using System.Globalization;

namespace M008_Lokalisierung.Controllers;

/// <summary>
/// IStringLocalizer: Entnimmt Lokalisierte Strings (muss per DI eingebunden werden, siehe Program.cs)
/// </summary>
public class HomeController(ILogger<HomeController> logger, IStringLocalizer<HomeController> localizer) : Controller
{
	public IActionResult Index()
	{
		CultureInfo.CurrentCulture = new CultureInfo("fr");
		CultureInfo.CurrentUICulture = new CultureInfo("fr");

		//CultureInfo beeinflusst den StringLocalizer
		LocalizedString str = localizer["Hello"];
		return View("Index", str.Value);
	}

	public IActionResult Privacy()
	{
		LocalizedString str = localizer["World"];
		return View("Privacy", str.Value);
	}

	public IActionResult SelectLanguage(string code, string returnUrl)
	{
		CultureInfo.CurrentCulture = new CultureInfo(code);
		CultureInfo.CurrentUICulture = new CultureInfo(code);

		HttpContext.Response.Cookies.Append("lang", code);

		//Hier wird ein Reload der Page gemacht (lädt die lokalisierten Strings)
		return View(returnUrl);
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}