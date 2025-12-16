using M007_ModelBinding.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace M007_ModelBinding.Controllers;

/// <summary>
/// Daten übertragen zw. Frontend und Backend
/// Probleme:
/// - Model kann nur ein Objekt enthalten
/// - name funktioniert nur in Forms
/// 
/// Lösung: asp-for
/// </summary>
public class HomeController(ILogger<HomeController> logger) : Controller
{
	/// <summary>
	/// From-Attribute
	/// Fangen Daten aus verschiedenen Quellen ein
	/// [FromQuery], [FromRoute], [FromForm], [FromBody], [FromHeader]
	/// </summary>
	[FromQuery]
	public string Text { get; set; }

	/// <summary>
	/// FromQuery kann hier auch ein Objekt zusammenbauen
	/// 
	/// https://localhost:7247/Home/Index?Username=Lukas&Password=123
	/// </summary>
	public IActionResult Index([FromQuery] User u)
	{
		//User u = new User("Lukas", "123");
		return View(u);
	}

	/// <summary>
	/// asp-route-...
	/// 
	/// Leitet Daten, welche NICHT im Objekt vorkommen vom Frontend ins Backend weiter
	/// Siehe _Layout.cshtml
	/// 
	/// WICHTIG: Variablenname im Frontend muss mit Variablenname im Backend übereinstimmen
	/// asp-route-x="..." -> int x
	/// </summary>
	public IActionResult Privacy(int x)
	{
		return View();
	}

	/// <summary>
	/// [Bind]: Ermöglicht Model-Binding vom Frontend ins Backend
	/// Generell nicht notwendig, aber manchmal muss es gesetzt werden
	/// </summary>
	public IActionResult Speichern([Bind] User u)
	{
		return View("Index");
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
