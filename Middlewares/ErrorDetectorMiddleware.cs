using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TodoList.Middlewares 
{
    public class ErrorDetectorMiddleware 
    {
        private readonly RequestDelegate _next;

        public ErrorDetectorMiddleware(RequestDelegate next) 
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context) 
        {

            System.Console.WriteLine("Request: " + context.Request.ToString());

            await _next(context);
        }
    }
}