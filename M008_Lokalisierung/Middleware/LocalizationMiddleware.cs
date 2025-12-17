using Microsoft.Extensions.Primitives;
using System.Globalization;

namespace M008_Lokalisierung.Middleware;

/// <summary>
/// Grundaufbau:
/// - Konstruktor mit RequestDelegate als Parameter (DI)
/// - InvokeAsync(HttpContext)
/// - await next
/// </summary>
public class LocalizationMiddleware(RequestDelegate next)
{
	public async Task InvokeAsync(HttpContext context)
	{
		string code = context.Request.Cookies["lang"];

		StringValues val = context.Request.Query["lang"]; //Ein Query-Parameter kann mehrmals angegeben werden
		if (val.Count > 0)
		{
			code = val[0];
		}

		if (!string.IsNullOrEmpty(code))
		{
			CultureInfo.CurrentCulture = new CultureInfo(code);
			CultureInfo.CurrentUICulture = new CultureInfo(code);
		}

		await next.Invoke(context);
	}
}