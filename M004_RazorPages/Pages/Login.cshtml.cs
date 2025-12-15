using M004_RazorPages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M004_RazorPages.Pages;

public class LoginModel(List<User> users) : PageModel
{
	public IActionResult OnPostLoginUser(string user, string pw)
	{
		if (!users.Any(e => e.Username == user))
			return BadRequest();

		User foundUser = users.First(e => e.Username == user);
		if (foundUser.Password != pw)
			return Forbid();

		return RedirectToPage("Index", foundUser);
	}
}
