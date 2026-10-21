using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Microsoft.AspNetCore.Http;

public class HangfireAuthoricationMiddleware : IMiddleware, ITransientDependency
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        // If the page is loaded through iframe, the access_token is passed to QueryString for the first time.
        if (context.Request.Path.StartsWithSegments("/hangfire") && 
            context.User.Identity?.IsAuthenticated != true)
        {
            if (context.Request.Query.TryGetValue("access_token", out var accessTokens))
            {
                context.Request.Headers.Add("Authorization", accessTokens);
                context.Response.Cookies.Append("access_token", accessTokens);
            }
            else if (context.Request.Cookies.TryGetValue("access_token", out string tokens))
            {
                context.Request.Headers.Add("Authorization", tokens);
            }
        }
        await next(context);
    }
}
