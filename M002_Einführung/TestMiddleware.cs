
namespace M001;

public class TestMiddleware : IMiddleware
{
	public async Task InvokeAsync(HttpContext context, RequestDelegate next)
	{
		//...

		await next.Invoke(context);
	}
}
