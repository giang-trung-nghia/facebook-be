using Facebook.API.Handlers;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace Facebook.API.Middlewares
{
    public class FormatDateMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<FormatDateMiddleware> _logger;

        public FormatDateMiddleware(RequestDelegate next, ILogger<FormatDateMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var originalResponseBody = context.Response.Body;

            using (var memoryStream = new MemoryStream())
            {
                context.Response.Body = memoryStream;

                await _next(context); // Call the next middleware

                // If the response is JSON, process it
                if (context.Response.ContentType != null &&
                    context.Response.ContentType.Contains("application/json", StringComparison.OrdinalIgnoreCase))
                {
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    var responseBody = await new StreamReader(memoryStream).ReadToEndAsync();

                    // Format the response body
                    var formattedResponse = FormatDateOnly(responseBody);

                    context.Response.Body = originalResponseBody;
                    await context.Response.WriteAsync(formattedResponse);
                }
                else
                {
                    // If not JSON, just copy the original response back
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    await memoryStream.CopyToAsync(originalResponseBody);
                    context.Response.Body = originalResponseBody;
                }
            }
        }

        private string FormatDateOnly(string responseBody)
        {
            // Deserialize the JSON response to a .NET object
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var jsonElement = JsonSerializer.Deserialize<JsonElement>(responseBody, options);

            // Serialize it back with a custom converter for DateOnly
            var newOptions = new JsonSerializerOptions
            {
                Converters = { new DateOnlyJsonConverter() },
                WriteIndented = false
            };

            return JsonSerializer.Serialize(jsonElement, newOptions);
        }
    }
}
