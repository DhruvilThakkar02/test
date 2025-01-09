using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.BusinessLayer.JwtAuthentication.JwtHelper
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method)]
    public class Jwtattribute : Attribute,IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            dynamic user = context.HttpContext.Items["User"];

            var TokenIsValid = context.HttpContext.Items["IsTokenValid"];

            if (TokenIsValid == "True")
            {
                context.Result = new JsonResult(new { message = "token is required" })
                {
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            else if (user == null) 
            {
                context.Result = new JsonResult(new { message = "unauthorized" })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
        }
    }
}
