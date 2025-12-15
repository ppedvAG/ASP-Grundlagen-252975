using M004_RazorPages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M004_RazorPages.Pages;

/// <summary>
/// Razor Pages
/// 
/// Alternativer Aufbau zu MVC
/// Enthält statt Controller + View eine Page
/// </summary>
public class IndexModel(ILogger<IndexModel> logger) : PageModel
{
	public int Zahl;

	public User user;

	/// <summary>
	/// Bei Razor Pages wird bei den On-Methoden immer die Page zurückgegeben (kein return View())
	/// </summary>
	public void OnGet(User u)
	{
		Zahl = 5; //Daten werden hier nicht per Parameter weitergegeben, sondern als Feld in dieser Klasse
		user = u;
	}

	/// <summary>
	/// Die On-Methoden können auch ein IActionResult zurückgeben
	/// Hier wird statt View() Page() verwendet
	/// </summary>
	//public IActionResult OnGet()
	//{
	//	return Page();
	//}
}