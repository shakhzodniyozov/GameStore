using GameStore.BusinessLogic;

internal class ExceptionHandler
{
    private readonly RequestDelegate next;

    public ExceptionHandler(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (BusinessLogicException e)
        {
            context.Response.StatusCode = e.StatusCode;
            await context.Response.WriteAsync(e.Message);
        }
        catch (Exception e)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync(e.Message);
        }
    }
}