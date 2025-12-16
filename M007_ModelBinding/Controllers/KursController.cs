using M000_DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace M007_ModelBinding.Controllers;

public class KursController(KursDBContext db) : Controller
{
	public IActionResult Index() => View(db.Kurse);

	public IActionResult KursBearbeiten(int id)
	{
		Kurse k = db.Kurse.First(e => e.Id == id);
		return View("Edit", k);
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
