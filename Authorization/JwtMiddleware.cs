using Microsoft.Extensions.Options;
using TodoWebAPI.HelperMethods;
using TodoWebAPI.Repository;

namespace TodoWebAPI.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtConfig _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<JwtConfig> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateJwtToken(token);
            if (userId != null)
            {
                // attach user to context on successful jwt validation
                context.Items["User"] = userService.GetById((int)userId);
            }

            await _next(context);
        }
    }
}
