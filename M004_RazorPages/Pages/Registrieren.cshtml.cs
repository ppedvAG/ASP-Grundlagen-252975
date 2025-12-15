using M004_RazorPages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M004_RazorPages.Pages;

public class RegistrierenModel(List<User> users) : PageModel
{
	/// <summary>
	/// WICHTIG: Diese Methode muss mit OnPost beginnen
	/// </summary>
	public IActionResult OnPostCreateUser(string user, string pw)
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
		return RedirectToPage("Login");
	}
}