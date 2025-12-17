using M000_DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace M010_API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class KursController(KursDBContext db) : ControllerBase
{
	[HttpGet]
	[Route("all")]
	[Produces("application/json", "application/xml")]
	public IEnumerable<Kurse> GetAlleKurse()
	{
		return db.Kurse;
	}

	[HttpGet]
	[Route("byId")]
	public Kurse GetKursById(int id)
	{
		return db.Kurse.First(e => e.Id == id);
	}

	[HttpPost]
	[Route("neuerKurs")]
	public IActionResult PostNeuerKurs(Kurse k)
	{
		try
		{
			db.Kurse.Add(k);
			db.SaveChanges();
		}
		catch (SqlException ex)
		{
			return BadRequest();
		}

		return Ok();
	}
}
