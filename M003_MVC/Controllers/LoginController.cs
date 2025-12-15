using M003_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace M003_MVC.Controllers;

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
	public IActionResult LoginUser(string user, string pw)
	{
		if (!users.Any(e => e.Username == user))
			return BadRequest();

		User foundUser = users.First(e => e.Username == user);
		if (foundUser.Password != pw)
			return Forbid();

		return View("Index", foundUser); //Model: Daten an das HTML weiterleiten
	}
}