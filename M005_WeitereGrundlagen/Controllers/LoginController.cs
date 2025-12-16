using M005_WeitereGrundlagen.Models;
using Microsoft.AspNetCore.Mvc;

namespace M005_WeitereGrundlagen.Controllers;

/// <summary>
/// Beispiel: Simples Login Portal
/// 
/// Anforderungen:
/// - Registrieren-Page
/// - Login-Page
/// - DI-Container (für die User)
/// </summary>
public class LoginController(List<User> users) : Controller
{
	/// <summary>
	/// IActionResult
	/// Generell ein HTTP-Code
	/// Sendet den User weiter
	/// </summary>
	[HttpGet]
	public IActionResult Register()
	{
		return View(); //Gibt die View hinter der Methode zurück
	}

	[HttpGet]
	public IActionResult Login()
	{
		return View();
	}

	[HttpGet]
	public IActionResult Index()
	{
		string? token = HttpContext.Request.Cookies["loginToken"];
		if (token != null)
		{
			string[] credentials = token.Split(';');
			User u = new User(credentials[0], credentials[1]);
			return View(u);
		}

		return View();
	}

	[HttpPost]
	public IActionResult CreateUser(string user, string pw)
	{
		if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pw))
			return BadRequest();

		if (users.Any(e => e.Username == user))
			return BadRequest();

		User u = new User(user, pw);
		users.Add(u);

		//Normalerweise leitet die View auf eine View mit dem selben Methodennamen weiter
		//WICHTIG: Die Login() Methode wird hier übersprungen
		//Alternative: return RedirectToAction("Login");
		return View("Login");
	}

	[HttpPost]
	public IActionResult LoginUser(string user, string pw, string stayLoggedIn)
	{
		if (!users.Any(e => e.Username == user))
			return BadRequest();

		User foundUser = users.First(e => e.Username == user);
		if (foundUser.Password != pw)
			return Forbid();

		if (stayLoggedIn == "on")
		{
			HttpContext.Response.Cookies.Append("loginToken", $"{user};{pw}");
		}

		return View("Index", foundUser); //Model: Daten an das HTML weiterleiten
	}
}