using M006_EntityFramework.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace M006_EntityFramework.Controllers;

public class HomeController(ILogger<HomeController> logger, KursDBContext db) : Controller
{
	public IActionResult Index()
	{
		IQueryable<Kurse> k = db.Kurse; //Hier werden noch keine Daten geladen

		List<Kurse> kurse = k.ToList(); //Hier werden die Daten tatsächlich geladen

		return View(kurse);
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
}
