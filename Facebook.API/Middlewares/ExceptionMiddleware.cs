using Facebook.Domain.Exceptions;

namespace Facebook.API.Middlewares
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
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        public async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            if (exception is NotFoundException notFoundException)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync(
                    text: new NotFoundException(notFoundException.DevMessage, notFoundException.UserMessage)
                    .ToString()
                );
                _logger.Log(LogLevel.Error, notFoundException.DevMessage);
            }
            else if (exception is ConflictException conflictException)
            {
                context.Response.StatusCode = StatusCodes.Status409Conflict;
                await context.Response.WriteAsync(
                    text: new ConflictException(conflictException.DevMessage, conflictException.UserMessage)
                    .ToString()
                );
                _logger.Log(LogLevel.Error, conflictException.DevMessage);
            }
            else if (exception is BusinessException businessException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync(
                    text: new BusinessException(businessException.DevMessage, businessException.UserMessage)
                    .ToString()
                );
                _logger.Log(LogLevel.Error, businessException.DevMessage);
            }
            else if (exception is UnauthorizationException unauthorException)
            {
                context.Response.StatusCode = StatusCodes.Status406NotAcceptable;
                await context.Response.WriteAsync(
                    text: new BusinessException(unauthorException.DevMessage, unauthorException.UserMessage)
                    .ToString()
                );
                _logger.Log(LogLevel.Error, unauthorException.DevMessage);
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync(
                    text: new BaseException(context.Response.StatusCode, exception.Message, "Something Error Internal Server!")
                     .ToString()
                );
                _logger.Log(LogLevel.Error, exception.Message);
            }
        }
    }
}
