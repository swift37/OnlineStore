using OnlineStore.MVC.Models.Exceptions;

namespace OnlineStore.MVC.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            switch (exception)
            {
                case ApiAuthenticationException apiAuthenticationException:
                    context.Response.Redirect($"/auth/refresh?redirectUrl={apiAuthenticationException.SourceUrl}");
                    break;
                default:
                    context.Response.Redirect($"/error");
                    break;
            }
        }
    }
}
