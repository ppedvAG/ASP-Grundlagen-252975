using M005_WeitereGrundlagen.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace M005_WeitereGrundlagen.Controllers;

public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;

	public HomeController(ILogger<HomeController> logger)
	{
		_logger = logger;
	}

	public IActionResult Index()
	{
		return View();
	}

	public IActionResult Privacy()
	{
		return View();
	}

	public IActionResult FileUpload(IFormFile file)
	{
		using StreamWriter sw = new StreamWriter(file.FileName);
		file.CopyTo(sw.BaseStream);
		sw.Flush();

		return View("Index");
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
