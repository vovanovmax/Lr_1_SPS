using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace WebApplication2.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _logFilePath;

        public RequestLoggingMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;

            var logDirectory = Path.Combine(env.ContentRootPath, "Logs");

            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            _logFilePath = Path.Combine(logDirectory, "requests.log");
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var url = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}";

            var time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            var ip = context.Connection.RemoteIpAddress?.ToString();

            var logMessage = $"[{time}] IP: {ip} | URL: {url}\n";

            await File.AppendAllTextAsync(_logFilePath, logMessage);

            await _next(context);
        }
    }
}
