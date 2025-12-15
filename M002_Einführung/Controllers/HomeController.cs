using System.Diagnostics;
using M001.Models;
using Microsoft.AspNetCore.Mvc;

namespace M001.Controllers;

/// <summary>
/// Ab C# 12: Primärer Konstruktor
/// </summary>
public class HomeController(ILogger<HomeController> logger, DITest x) : Controller
{
	//private readonly ILogger<HomeController> _logger;

	//private readonly DITest _userCounter;

	/// <summary>
	/// Hier im Konstruktor wird das Objekt von DI in Program.cs empfangen
	/// </summary>
	//public HomeController(ILogger<HomeController> logger, DITest x)
	//{
	//	_logger = logger;
	//	_userCounter = x;
	//}

	public IActionResult Index()
	{
		x.Zaehler++;
		return View();
	}

	public IActionResult Privacy()
	{
		x.Zaehler++;
		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
