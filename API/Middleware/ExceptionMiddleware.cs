namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);

                if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
                {
                    await HandleExceptionAsync(context, StatusCodes.Status403Forbidden, "Доступ запрещён (403)");
                }
                else if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
                {
                    await HandleExceptionAsync(context, StatusCodes.Status401Unauthorized, "Не авторизован (401)");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка");
                await HandleExceptionAsync(context, StatusCodes.Status500InternalServerError, "Ошибка сервера (500)" + ex.ToString());
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, int statusCode, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(new
            {
                error = message,
                statusCode
            }));
        }
    }

}
