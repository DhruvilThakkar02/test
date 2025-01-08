using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.User.Login.LoginRequestDtos;
using HRMS.Dtos.User.Login.LoginResponseDtos;
using HRMS.Utility.Helpers.Enums;
using HRMS.Utility.Helpers.Handlers;
using Swashbuckle.AspNetCore.Annotations;

namespace HRMS.API.Endpoints.User
{
    public static class LoginEndpoints
    {
        public static void MapLoginEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/login", async (LoginRequestDto request, ILoginService loginService) =>
            {
                //var validator = new LoginRequestValidator(); 
                //var validationResult = validator.Validate(request);

                //if (!validationResult.IsValid)
                //{
                //    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                //    return Results.BadRequest(
                //        ResponseHelper<List<string>>.Error(
                //            message: "Validation Failed",
                //            errors: errorMessages,
                //            statusCode: StatusCode.BAD_REQUEST
                //        ).ToDictionary()
                //    );
                //}

                try
                {
                    var loginResponse = await loginService.Login(request);

                    if (!string.IsNullOrEmpty(loginResponse.ErrorMessage))
                    {
                        return Results.BadRequest(
                            ResponseHelper<string>.Error(
                                message: loginResponse.ErrorMessage,
                                statusCode: StatusCode.BAD_REQUEST
                            ).ToDictionary()
                        );
                    }
                    return Results.Ok(
                        ResponseHelper<LoginResponseDto>.Success(
                            message: "Login Successful",
                            data: loginResponse
                        ).ToDictionary()
                    );
                }
                catch (Exception ex)
                {
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while logging in.",
                            exception: ex,
                            isWarning: false,
                            statusCode: StatusCode.INTERNAL_SERVER_ERROR
                        ).ToDictionary()
                    );
                }
            }).WithTags("Authentication")
    .WithMetadata(new SwaggerOperationAttribute(
        summary: "User Login",
        description: "This endpoint allows you to login with your username/email and password."
    ));

        }


    }
}
