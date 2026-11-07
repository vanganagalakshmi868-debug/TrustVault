namespace TrustVault.Api.Middleware;
public class AuthorizationMiddleware
{
    private readonly RequestDelegate _next;
    public AuthorizationMiddleware(RequestDelegate next) => _next = next;
    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);
        if (context.Response.StatusCode == 403)
        {
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync("{\"error\":\"Access denied - You don't have permission\"}");
        }
    }
}