using M003_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace M003_MVC.Controllers;

public class HomeController(ILogger<HomeController> logger) : Controller
{
	public IActionResult Index()
	{
		return View();
	}

	public IActionResult Privacy()
	{
		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}

	public IActionResult Berechne(string z1, string z2, Rechenarten rechenart)
	{
		if (!float.TryParse(z1, out float zahl1) || !float.TryParse(z2, out float zahl2))
			return BadRequest();

		float ergebnis = rechenart switch
		{
			Rechenarten.Addieren => zahl1 + zahl2,
			Rechenarten.Subtrahieren => zahl1 - zahl2,
			Rechenarten.Multiplizieren => zahl1 * zahl2,
			Rechenarten.Dividieren => zahl2 != 0 ? zahl1 / zahl2 : 0
		};

		return View("Ergebnis", ergebnis);
	}
}

public enum Rechenarten { Addieren, Subtrahieren, Multiplizieren, Dividieren }