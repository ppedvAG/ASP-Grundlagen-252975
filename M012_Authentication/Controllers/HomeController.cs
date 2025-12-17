using M012_Authentication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace M012_Authentication.Controllers;

public class HomeController(
	ILogger<HomeController> logger,
	SignInManager<IdentityUser> sim,
	UserManager<IdentityUser> um,
	RoleManager<IdentityRole> rm) : Controller
{
	public async Task<IActionResult> Index()
	{
		await sim.SignInAsync(new IdentityUser() { UserName = "Admin2", Email = "admin2@example.com" }, true);

		return View();
	}

	public async Task<IActionResult> Privacy()
	{
		//User erzeugen per C#-Code
		IdentityUser adminUser = new IdentityUser() { UserName = "Admin2", Email = "admin2@example.com" };
		if (await um.FindByNameAsync("admin2@example.com") == null)
		{
			IdentityResult result = await um.CreateAsync(adminUser);
			IdentityResult pw = await um.AddPasswordAsync(adminUser, "admin123");

			if (!result.Succeeded || !pw.Succeeded)
				return BadRequest();
		}

		////////////////////////////////////////////////////////////

		//Admin Rolle erzeugen und zuweisen
		IdentityRole adminRole = new IdentityRole("Admin2");
		if (!await rm.RoleExistsAsync(adminRole.Name))
		{
			await rm.CreateAsync(adminRole);
		}

		IdentityResult roleResult = await um.AddToRoleAsync(adminUser, adminRole.Name);

		//Einzelne Rechte verwalten
		Claim c = new Claim("Erstellen", "True");
		await rm.AddClaimAsync(adminRole, c);

		return View();
	}

	public async Task<IActionResult> AdminPortal()
	{
		IdentityUser u = await um.FindByNameAsync(HttpContext.User.Identity.Name);
		IdentityResult res = await um.AddToRoleAsync(u, "Admin2");

		//Option 1: Rollen
		//if (HttpContext.User.IsInRole("Admin2"))
		if (await um.IsInRoleAsync(u, "Admin2"))
			return View();
		else
			return Unauthorized();

		///////////////////////////////////////////////////////
		//Option 2: Claims
		if (HttpContext.User.FindFirstValue("Erstellen") == "True")
			return View();
		else
			return Unauthorized();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
