
namespace WebAppMovies.MiddleWare
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AppException appEx)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(new { error = $"Unhandled exception : {appEx.Message}" });
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new { error = $"Internal server error : {ex.Message}" });
            }
        }
    }
}
