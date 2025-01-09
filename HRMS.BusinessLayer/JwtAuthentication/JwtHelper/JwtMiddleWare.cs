using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.User.User.UserResponseDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
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
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            
           
            if (token == null)
            {
                
                context.Items["IsTokenValid"] = "True";
            }
            else
            {
                var user = await AttachUserToContext(context, token, userService);

                if (user != null)
                {
                    context.Items["UserReadResponseDto"] = user;
                }
            }
            await _next(context);
        }

        public async Task<UserReadResponseDto> AttachUserToContext(HttpContext context, string token, IUserService userService)
        {


            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("THIS IS USED TO SIGN AND VERIFY JWT TOKENS, REPLACE IT WITH YOUR OWN SECRET,IT CAN BE ANY STRING");
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "UserId").Value);               

                var user = await userService.GetUserById(userId);
                return user;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);


            }
        }


    }
}
