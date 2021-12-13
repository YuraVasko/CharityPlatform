using CharityPlatform.SharedKernel;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace CharityPlatform.API.Infrastructure
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch(Error domainError)
            {
                var response = new { error = domainError.Message };
                await context.Response.WriteAsJsonAsync(response);
            }
            catch(Exception ex)
            {
                var response = new { error = "Some error occures on server side." };
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
