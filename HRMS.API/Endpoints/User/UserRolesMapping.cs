using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.User.User.UserResponseDtos;
using HRMS.Dtos.User.UserRoles.UserRolesRequestDtos;
using HRMS.Dtos.User.UserRoles.UserRolesResponseDtos;
using HRMS.Dtos.User.UserRolesMapping.UserRolesMappingRequestDtos;
using HRMS.Dtos.User.UserRolesMapping.UserRolesMappingResponseDtos;
using HRMS.Utility.Helpers.Enums;
using HRMS.Utility.Helpers.Handlers;
using HRMS.Utility.Validators.User.UserRoles;
using HRMS.Utility.Validators.User.UserRolesMapping;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Endpoints.User
{
    public static class UserRolesMapping
    {
        public static void MapUserRolesMappingEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("HRMS/GetAllUserRolesMapping", async (IUserRolesMappingService _rolesmappingService) =>
            {
                var rolesmapping = await _rolesmappingService.GetAllUserRolesMapping();
                if (rolesmapping != null && rolesmapping.Any())
                {
                    var response = ResponseHelper<List<UserRolesMappingReadResponseDto>>.Success("User Roles Mapping Retrieved Successfully ", rolesmapping.ToList());
                    return Results.Ok(response.ToDictionary());
                }

                var errorResponse = ResponseHelper<List<UserReadResponseDto>>.Error("No User Roles Mapping Roles Found");
                return Results.NotFound(errorResponse.ToDictionary());
            }).WithTags("UserRolesMapping");

            app.MapGet("/HRMS/GetByIdUserRolesMapping{id}", async (IUserRolesMappingService _rolesmappingService, int id) =>
            {
                
                try
                {
                    var rolemapping = await _rolesmappingService.GetByIdUserRolesMapping(id);
                    if (rolemapping == null)
                    {
                        return Results.NotFound(
                            ResponseHelper<string>.Error(
                                message: "User Role Mapping Not Found ",
                                statusCode: StatusCodeEnum.NOT_FOUND
                                ).ToDictionary()
                                );
                    }

                    return Results.Ok(
                        ResponseHelper<UserRolesMappingReadResponseDto>.Success(
                            message: "User Roles Mapping Retrieved Successfully",
                            data: rolemapping
                            ).ToDictionary()
                        );

                }
                catch (Exception ex)
                {
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred.",
                            exception: ex,
                            isWarning: false,
                            statusCode: StatusCodeEnum.INTERNAL_SERVER_ERROR
                        ).ToDictionary()
                    );
                }
            }).WithTags("UserRolesMapping");
            app.MapPost("HRMS/CreateUserRolesMapping", async (UserRolesMappingCreateRequestDto dto, IUserRolesMappingService _rolesmappingService) =>
            {
                var validator = new UserRolesMappingCreateRequestValidator();
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return Results.BadRequest(
                        ResponseHelper<List<string>>.Error(
                            message: "Validation Failed",
                            errors: errorMessages,
                            statusCode: StatusCodeEnum.BAD_REQUEST
                        ).ToDictionary()
                    );
                }

                try
                {
                    var newrolemapping = await _rolesmappingService.CreateUserRolesMapping(dto);
                    return Results.Ok(
                        ResponseHelper<UserRolesMappingCreateResponseDto>.Success(
                            message: "User Role Mapping Created Successfully",
                            data: newrolemapping
                        ).ToDictionary()
                    );
                }
                catch (Exception ex)
                {
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Creating the Role.",
                            exception: ex,
                            isWarning: false,
                            statusCode: StatusCodeEnum.INTERNAL_SERVER_ERROR
                        ).ToDictionary()
                    );
                }
            }).WithTags("UserRolesMapping");

            app.MapPut("/HRMS/UpdateUserRolesMapping", async (IUserRolesMappingService _rolesmappingService, UserRolesMappingUpdateRequestDto dto) =>
            {
                try
                {
                    var updatedUserMappingRoles = await _rolesmappingService.UpdateUserRolesMapping(dto);
                    if (updatedUserMappingRoles == null)
                    {
                        return Results.NotFound(
                            ResponseHelper<string>.Error(
                                message: "User Mapping Roles Not Found",
                                statusCode: StatusCodeEnum.NOT_FOUND
                            ).ToDictionary()
                         );
                    }
                    return Results.Ok(
                        ResponseHelper<UserRolesMappingUpdateResponseDto>.Success(
                            message: "User Roles Updated Succesfully ",
                            data: updatedUserMappingRoles
                            ).ToDictionary()
                        );
                }
                catch (Exception ex)
                {
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Updating the User Mapping Roles.",
                            exception: ex,
                            isWarning: false,
                            statusCode: StatusCodeEnum.INTERNAL_SERVER_ERROR
                        ).ToDictionary()
                    );

                }
            }).WithTags("UserRolesMapping");

            //app.MapPut(/UpdateRolesMapping")Future Enhansement
            //app.MapDelete(/DeleteRolesMapping")Future Enhansement
        }
    }
}
