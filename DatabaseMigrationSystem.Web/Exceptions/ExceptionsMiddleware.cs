using Action.Platform.Common.Exceptions;
using DatabaseMigrationSystem.Common;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace DatabaseMigrationSystem.Exceptions;

/// <summary>
/// Exceptions middleware.
/// </summary>
internal sealed class ExceptionsMiddleware : IMiddleware
{
    
    /// <summary>
    /// Initializes a new instance.
    /// </summary>
    public ExceptionsMiddleware()
    {
      
    }


    /// <inheritdoc />
    public async Task InvokeAsync(HttpContext context,
        RequestDelegate next)
    {
        if (!next.IsNull())
        {
            try
            {
                await next.Invoke(context);
            }
            catch (BrokenRulesException exc)
            {
                context.Response.Headers["Content-Type"] = "application/json; charset=utf-8";
                context.Response.StatusCode = 400;

                await context.Response
                    .WriteAsync(JsonConvert.SerializeObject(exc.BrokenRules))
                    .ConfigureAwait(false);
            }
            catch (Exception exc)
            {
                
                await ModifyResponse(context, exc);
            }
        }
    }

    private async Task ModifyResponse (HttpContext context, Exception exc)
    {
        context.Response.Headers["Content-Type"] = "application/json; charset=utf-8";
        context.Response.StatusCode = 500;

        await context.Response
            .WriteAsync(JsonConvert.SerializeObject(exc))
            .ConfigureAwait(false);
        }
}