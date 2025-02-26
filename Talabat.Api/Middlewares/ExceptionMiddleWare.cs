using System.Net;
using System.Text.Json;
using Talabat.Api.Error;

namespace Talabat.Api.Middlewares
{
    // This class is for handling exceptions in the application to internal server error
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleWare> _logger;
        private readonly IHostEnvironment _env;
        public ExceptionMiddleWare(RequestDelegate next, ILogger<ExceptionMiddleWare> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }
        //InvokeAsync method to handle exceptions
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var Response = _env.IsDevelopment()
                    ? new ApiExceptionResponce((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                    : new ApiExceptionResponce((int)HttpStatusCode.InternalServerError);
                var jsonResponse = JsonSerializer.Serialize(Response);
             await context.Response.WriteAsync(jsonResponse);
            }
        }


    }
}
