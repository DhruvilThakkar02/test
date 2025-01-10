using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.User.User.UserResponseDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


namespace HRMS.BusinessLayer.JwtAuthentication.JwtHelper
{
    public class JwtMiddleWare
    {
        private readonly RequestDelegate _next;

        public JwtMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            var endpointFeature = context.Features.Get<IEndpointFeature>();
            var endpoint = endpointFeature?.Endpoint;

            var allowAnonymous = endpoint?.Metadata?.GetMetadata<AllowAnonymousAttribute>() != null;

            if (allowAnonymous)
            {
                await _next(context);
            }
            else if (!context.Request.Headers.TryGetValue("Authorization", out StringValues token) || string.IsNullOrEmpty(token))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized: Token is missing or empty");
            }
            else
            {
                try
                {
                    var splitToken = token.ToString().Split(" ");
                    var extractedToken = splitToken[splitToken.Length - 1];
                    var user = await AttachUserToContext(context, extractedToken, userService);
                    if (user != null)
                    {
                        context.Items["UserReadResponseDto"] = user;
                        context.Items["IsTokenValid"] = "True"; 
                    }
                    else
                    {
                        await HandleInvalidToken(context, "Invalid token or user not found");
                        return;
                    }
                }
                catch (SecurityTokenExpiredException)
                {
                   await HandleInvalidToken(context, "Token has expired");
                    return;
                }
                catch (SecurityTokenException)
                {
                   await HandleInvalidToken(context, "Invalid token");
                    return;
                }
                catch (Exception ex)
                {
                   await HandleInvalidToken(context, $"Unauthorized: {ex.Message}");
                    return;
                }

                await _next(context);
            }
        }

        public async Task<UserReadResponseDto> AttachUserToContext(HttpContext context, string token, IUserService userService)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("THIS IS USED TO SIGN AND VERIFY JWT TOKENS, REPLACE IT WITH YOUR OWN SECRET, IT CAN BE ANY STRING");
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
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while validating the token.", ex);
            }
        }

        private static async Task HandleInvalidToken(HttpContext context, string errorMessage)
        {
            context.Items["IsTokenValid"] = "False"; 
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync(errorMessage);
 
        }
    }

   
}
