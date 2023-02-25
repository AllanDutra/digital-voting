using System.Diagnostics;
using System.Net;
using DigitalVoting.Shared.Exceptions;
using DigitalVoting.Shared.Responses;

namespace DigitalVoting.API.Middlewares
{
    public class GlobalExceptionHandler : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            if (context != null)
            {
                if (exception is ResponseException)
                {
                    ResponseException ex = (ResponseException)exception;
                    context.Response.StatusCode = (int)ex.StatusCode;

                    ResponseDetails.Message = ex.Message;
                    ResponseDetails.StatusCode = ex.StatusCode;
                }
                else
                {
                    ResponseDetails.Message = "Tivemos um problema interno no servidor. Tente novamente mais tarde!";
                    ResponseDetails.StatusCode = HttpStatusCode.InternalServerError;
                }

                context.Response.StatusCode = (int)ResponseDetails.StatusCode;

                StackTrace st = new StackTrace(exception, true);

                StackFrame frame = st.GetFrame(0).GetFileName() is null ? st.GetFrame(1) : st.GetFrame(0);

                string fileName = frame.GetFileName();

                string methodName = st.GetFrame(1).GetMethod().ReflectedType.Name;

                int line = frame.GetFileLineNumber();

                int col = frame.GetFileColumnNumber();

                await context.Response.WriteAsJsonAsync(new DefaultResponse(ResponseDetails.Message));
            }
        }
        public static class ResponseDetails
        {
            public static string Message { get; set; }
            public static HttpStatusCode StatusCode { get; set; }

        }
    }
}