namespace KnowledgeTestCore.MiddleWare
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next,
                                 ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation("➡️ {Method} {Path} | {Time}",
                context.Request.Method,
                context.Request.Path,
                DateTime.UtcNow);

            await _next(context);

            _logger.LogInformation("✅ Response: {StatusCode}",
                context.Response.StatusCode);
        }

    }
}
