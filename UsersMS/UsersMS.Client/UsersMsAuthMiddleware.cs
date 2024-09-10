using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UsersMS.Client
{
    public class UsersMsAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public UsersMsAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            

            // Call the next delegate/middleware in the pipeline.
            await _next(context);
        }
    }

}
