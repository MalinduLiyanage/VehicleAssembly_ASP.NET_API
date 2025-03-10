using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Vehicle_Assembly.DTOs.Responses;
using Vehicle_Assembly.Utilities.JwtUtility;

namespace Vehicle_Assembly.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate next;

        public JwtMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {

            string? token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ")[1];

            if (token == null)
            {
                if (IsEnabledUnauthorizedRoute(httpContext))
                {
                    return next(httpContext);
                }

                BaseResponse response = new BaseResponse
                {
                    status_code = StatusCodes.Status401Unauthorized,
                    data = new { message = "Unauthorized Request" }
                };
                httpContext.Response.StatusCode = response.status_code;
                httpContext.Response.ContentType = "application/json";

                return httpContext.Response.WriteAsJsonAsync(response);
            }
            else
            {
                if (JwtTokenValidation.ValidateJwtToken(token))
                {
                    return next(httpContext);
                }
                else
                {
                    BaseResponse response = new BaseResponse
                    {
                        status_code = StatusCodes.Status401Unauthorized,
                        data = new { message = "Unauthorized Request" }
                    };
                    httpContext.Response.StatusCode = response.status_code;
                    httpContext.Response.ContentType = "application/json";

                    return httpContext.Response.WriteAsJsonAsync(response);
                }
            }
        }

        private bool IsEnabledUnauthorizedRoute(HttpContext httpContext)
        {
            var enabledRoutes = new Dictionary<string, string>
            {
                { "/api/DBTest", "GET" },
                { "/api/Admin", "POST" },
                { "/api/AdminLogin", "POST" },
            };

            if (httpContext.Request.Path.Value is not null)
            {
                if (httpContext.Request.Path.Value.StartsWith("/uploads/", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                foreach (var route in enabledRoutes)
                {
                    if (httpContext.Request.Path.Value.Equals(route.Key, StringComparison.OrdinalIgnoreCase) &&
                        httpContext.Request.Method.Equals(route.Value, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

    }

    public static class JwtMiddlewareExtensions
    {
        public static IApplicationBuilder UseJwtMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtMiddleware>();
        }
    }
}
