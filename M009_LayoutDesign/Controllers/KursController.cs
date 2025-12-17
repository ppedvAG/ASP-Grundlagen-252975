using M000_DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace M009_LayoutDesign.Controllers;

public class KursController(KursDBContext db) : Controller
{
	public IActionResult Index()
	{
		return View(db.Kurse);
	}

	public async Task<IActionResult> Speichern(Kurse k)
	{
		if (!ModelState.IsValid) //Sind alle gegebenen Werte DataAnnotation-konform?
		{
			return BadRequest();
		}

		db.Kurse.Update(k);
		await db.SaveChangesAsync();

		return View("Index", db.Kurse);
	}
}
