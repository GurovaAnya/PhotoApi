using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PhotoApi.Exceptions;

namespace PhotoApi.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        public async Task Invoke(HttpContext httpContext)
        {
            var ex = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
            HttpStatusCode statusCode;
            
            if (ex is NotFoundException) statusCode = HttpStatusCode.NotFound;
            else if (ex is BadRequestException) statusCode = HttpStatusCode.BadRequest;
            else statusCode = HttpStatusCode.InternalServerError;
            var wrappedException = new ExceptionWrapper(ex);
            wrappedException.StatusCode = (int)statusCode;
            httpContext.Response.StatusCode = (int) statusCode;
            httpContext.Response.ContentType = "application/json";
            await using var writer = new StreamWriter(httpContext.Response.Body);
            new JsonSerializer().Serialize(writer, wrappedException);
            await writer.FlushAsync().ConfigureAwait(false);
        }
    }
}