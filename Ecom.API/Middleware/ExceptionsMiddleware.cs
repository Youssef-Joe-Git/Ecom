using Ecom.API.Helper;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Text.Json;

namespace Ecom.API.Middleware
{
    public class ExceptionsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _env;
        private readonly IMemoryCache _cache;

        public ExceptionsMiddleware(RequestDelegate next, IHostEnvironment env, IMemoryCache cache)
        {
            _next = next;
            _env = env;
            _cache = cache;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                ApplySecurityHeaders(context);
                if (!IsRequstAllowed(context))
                {
                    context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                    context.Response.ContentType = "application/json";
                    var response = new ApiExceptions(context.Response.StatusCode, "Too many requests. Please try again later.");
                    var jsonResponse = JsonSerializer.Serialize(response);
                    await context.Response.WriteAsync(jsonResponse);
                    return;
                }
                await _next(context);
            }
            catch (Exception ex)
            {
                var statusCode = ex switch
                {
                    KeyNotFoundException => (int)HttpStatusCode.NotFound,
                    UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                    ArgumentException => (int)HttpStatusCode.BadRequest,
                    _ => (int)HttpStatusCode.InternalServerError
                };

                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/json";
                var response = _env.IsDevelopment() ?
                    new ApiExceptions((int)context.Response.StatusCode, ex.Message, ex.StackTrace) :
                    new ApiExceptions((int)context.Response.StatusCode, ex.Message);
                var jsonResponse = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(jsonResponse);
            }
        }
        public bool IsRequstAllowed(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress.ToString();
            var cacheKey = $"RateLimit_{ip}";
            var dateTimeNow = DateTime.UtcNow;

            var (timesTamp, count) = _cache.GetOrCreate(cacheKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
                return (dateTimeNow, 0);

            });

            if (dateTimeNow - timesTamp < TimeSpan.FromSeconds(30))
            {
                if (count >= 5)
                {
                    return false;
                }
                _cache.Set(cacheKey, (timesTamp, count + 1), TimeSpan.FromSeconds(30));
            }
            else
            {
                _cache.Set(cacheKey, (dateTimeNow, count), TimeSpan.FromSeconds(30));
            }
            return true;

        }
        public void ApplySecurityHeaders(HttpContext context)
        {
            context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
            context.Response.Headers.Add("X-Frame-Options", "DENY");
            context.Response.Headers.Add("Referrer-Policy", "no-referrer");
            context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'; script-src 'self'; style-src 'self'; img-src 'self'; font-src 'self'; connect-src 'self'");
        }
    }
}
