using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace sistemaventa.Middlewares
{
    public class ExceptionMiddleware
    {
        // requestDelegate representa el middleware en el pipeline
        private readonly RequestDelegate _next;

        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        // metodo principal que se ejecuta en cada solicitud http

        public async Task InvokeAsync(HttpContext context)
        {
            try 
            {
                // capturaramos cualquier excepcion
                await _next(context);
            }
            catch (Exception ex)
            {
                // registramos la excepcion
                _logger.LogError(ex, "Se capturo una excepcion inesperada.");
                // generamos una respuesta http
                await HandleExceptionAsync(context, ex);
            }
        }

        // metodo que construye una respuesta http en formato JSON cuando ocurre una excepcion.
        public static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // obtengo la ruta donde ocurrio el error
            var path = context.Request.Path;
            context.Response.ContentType = "application/json";

            // asigna un codigo de estado http segun el tipo de excepcion
            context.Response.StatusCode = exception switch
            {
                KeyNotFoundException => (int)HttpStatusCode.NotFound,                             // 404: recurso no encontrado
                ArgumentException or ArgumentNullException => (int)HttpStatusCode.BadRequest,     // 400: datos invalidos
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,                  // 401: no autorizado
                DbUpdateException => (int)HttpStatusCode.InternalServerError,                     // 500: error de base de datos
                _ => (int)HttpStatusCode.InternalServerError,                                     // 500: error generico
            };

            // construimos el error personalizado 
            var errorResponse = new
            {
                Statuscode = context.Response.StatusCode, // codigo de estado HTTP
                Message = context.Response.StatusCode == 500 ? "Ocurrio un error interno en el servidor" : exception.Message,
                Detail = context.Response.StatusCode == 500 && !context.Request.Host.Host.Contains("localhost") ? "Contacte al administrador del sistema." : exception.Message,
                Path = path,
                Timestamp = DateTime.UtcNow,
            };

            // serializar el JSON en camelCase
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var errorJson = JsonSerializer.Serialize(errorResponse, options);

            // escribir la respuesta en el cuerpo de la respuesta HTTP
            await context.Response.WriteAsync(errorJson);
        }     
    }
}
