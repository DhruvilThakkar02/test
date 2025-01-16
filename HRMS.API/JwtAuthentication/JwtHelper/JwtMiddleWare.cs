using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.User.User.UserResponseDtos;
using HRMS.Utility.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


namespace HRMS.Utility.JwtAuthentication.JwtHelper
{

    public class JwtMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly JwtSecretKey _jwtSecretKey;


        public JwtMiddleWare(RequestDelegate next, IOptions<JwtSecretKey> jwtSecretKey)
        {
            _next = next;
            _jwtSecretKey = jwtSecretKey.Value;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            var endpointFeature = context.Features.Get<IEndpointFeature>();
            var endpoint = endpointFeature?.Endpoint;

            // Check if the endpoint allows anonymous access
            var allowAnonymous = endpoint?.Metadata?.GetMetadata<AllowAnonymousAttribute>() != null;

            if (allowAnonymous)
            {
                await _next(context);
                return;
            }

            // Check if Authorization header is present
            if (!context.Request.Headers.TryGetValue("Authorization", out StringValues token) || string.IsNullOrEmpty(token))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized: Token is missing or empty");
                return;
            }

            try
            {
                var splitToken = token.ToString().Split(" ");
                var extractedToken = splitToken[^1];
                var user = await AttachUserToContext(context, extractedToken, userService);

                if (user != null)
                {
                    context.Items["UserReadResponseDto"] = user;
                    context.Items["IsTokenValid"] = true;

                  
                    if (endpoint?.Metadata?.GetMetadata<AuthorizeAttribute>()?.Policy != null)
                    {
                        var requiredRolePolicy = endpoint.Metadata.GetMetadata<AuthorizeAttribute>().Policy;


                       

                        var userRole = ((JwtSecurityToken)new JwtSecurityTokenHandler().ReadToken(extractedToken))
                            .Claims.FirstOrDefault(x => x.Type == "UserRoleName")?.Value;

                        
                        if (userRole != requiredRolePolicy)
                        {
                            context.Response.StatusCode = StatusCodes.Status403Forbidden;
                            await context.Response.WriteAsync("Forbidden: You do not have sufficient roles.");
                            return;
                        }
                    }
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Unauthorized: Invalid token or user not found");
                    return;
                }
            }
            catch (SecurityTokenExpiredException)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized: Token has expired");
                return;
            }
            catch (SecurityTokenException)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized: Invalid token");
                return;
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync($"Unauthorized: {ex.Message}");
                return;
            }

            await _next(context);
        }

        private async Task<UserReadResponseDto> AttachUserToContext(HttpContext context, string token, IUserService userService)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecretKey.Secret);

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "UserId").Value);
            var user = await userService.GetUserById(userId);
            return user;
        }
    }
}
