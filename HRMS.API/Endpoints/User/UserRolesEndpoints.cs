using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.User.UserRoles.UserRolesRequestDtos;
using HRMS.Dtos.User.UserRoles.UserRolesResponseDtos;
using HRMS.Dtos.User.User.UserResponseDtos;
using HRMS.Utility.Helpers.Enums;
using HRMS.Utility.Helpers.Handlers;
using HRMS.Utility.Validators.User.UserRoles;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HRMS.API.Endpoints.User
{
    public static class UserRolesEndpoints
    {
        public static void MapUserRolesEndpoints(this IEndpointRouteBuilder app)
        {
            /// <summary> 
            /// Retrieves a List of User Roles. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint returns a List of User Roles. If no User Roles are found, a 404 status code is returned. 
            /// </remarks> 
            /// <returns>A List of User Roles or a 404 status code if no User Roles are found.</returns>
            app.MapGet("/GetUserRoles", async (IUserRolesService _rolesService) =>
            {
                var roles = await _rolesService.GetUserRoles();
                if (roles != null && roles.Any())
                {
                    var response = ResponseHelper<List<UserRolesReadResponseDto>>.Success("User Roles Retrieved Successfully ", roles.ToList());
                    return Results.Ok(response.ToDictionary());
                }

                var errorResponse = ResponseHelper<List<UserReadResponseDto>>.Error("No User Roles Found");
                return Results.NotFound(errorResponse.ToDictionary());
            }).WithTags("User Role")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Retrieves a List of User Roles", description: "This endpoint returns a List of User Roles. If no User Roles are found, a 404 status code is returned."
            ));

            /// <summary> 
            /// Retrieve User Role by Id. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint return User Role by Id. If no User Role are found, a 404 status code is returned. 
            /// </remarks> 
            /// <returns>A User Role or a 404 status code if no User Role are found.</returns>
            app.MapGet("/GetUserRoleById/{id}", async (IUserRolesService _rolesService, int id) =>
            {
                var validator = new UserRolesReadRequestValidator();
                var rolesRequestDto = new UserRolesReadRequestDto { UserRoleId = id };

                var validationResult = validator.Validate(rolesRequestDto);
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
                    var role = await _rolesService.GetUserRoleById(id);
                    if (role == null)
                    {
                        return Results.NotFound(
                            ResponseHelper<string>.Error(
                                message: "Role Not Found ",
                                statusCode: StatusCodeEnum.NOT_FOUND
                                ).ToDictionary()
                                );
                    }

                    return Results.Ok(
                        ResponseHelper<UserRolesReadResponseDto>.Success(
                            message: "Role Retrieved Successfully",
                            data: role
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
            }).WithTags("User Role")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Retrieve User Role by Id", description: "This endpoint return User Role by Id. If no User Role are found, a 404 status code is returned."
            ));

            /// <summary> 
            /// Creates a new User Role. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint allows you to create a new User Role with the provided details. 
            /// </remarks> 
            ///<returns> A success or error response based on the operation result.</returns >
            app.MapPost("/CreateUserRole", async (UserRolesCreateRequestDto dto, IUserRolesService _rolesService) =>
            {
                var validator = new UserRolesCreateRequestValidator();
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
                    var newRole = await _rolesService.CreateUserRole(dto);
                    return Results.Ok(
                        ResponseHelper<UserRolesCreateResponseDto>.Success(
                            message: "Role Created Successfully",
                            data: newRole
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
            }).WithTags("User Role")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Creates a new User Role.", description: "This endpoint allows you to create a new User Role with the provided details."
            ));

            /// <summary> 
            /// Updates existing User Role details. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint allows you to update User Role details with the provided Id. 
            /// </remarks> 
            ///<returns> A success or error response based on the operation result.</returns >
            app.MapPut("/UpdateUserRole", async (IUserRolesService _rolesService, [FromBody] UserRolesUpdateRequestDto dto) =>
            {
                var validator = new UserRolesUpdateRequestValidator();
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
                    var updatedUserRoles = await _rolesService.UpdateUserRoles(dto);
                    if (updatedUserRoles == null)
                    {
                        return Results.NotFound(
                            ResponseHelper<string>.Error(
                                message: "User Roles Not Found",
                                statusCode: StatusCodeEnum.NOT_FOUND
                            ).ToDictionary()
                         );
                    }
                    return Results.Ok(
                        ResponseHelper<UserRolesUpdateResponseDto>.Success(
                            message: "User Roles Updated Succesfully ",
                            data: updatedUserRoles
                            ).ToDictionary()
                        );
                }
                catch (Exception ex)
                {
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Updating the User Roles.",
                            exception: ex,
                            isWarning: false,
                            statusCode: StatusCodeEnum.INTERNAL_SERVER_ERROR
                        ).ToDictionary()
                    );

                }

            }).WithTags("User Role")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Updates existing User Role details", description: "This endpoint allows you to update User Role details with the provided Id."
            ));

            /// <summary> 
            /// Deletes a User Role. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint allows you to delete a User Role based on the provided User Role Id.</remarks>
            app.MapDelete("/DeleteUserRole", async (IUserRolesService _rolesService, [FromBody] UserRolesDeleteRequestDto dto) =>
            {
                var validator = new UserRolesDeleteRequestValidator();
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
                    var result = await _rolesService.DeleteUserRoles(dto);
                    if (result == null)
                    {
                        return Results.NotFound(
                            ResponseHelper<string>.Error(
                                message: "User Roles Not Found",
                                statusCode: StatusCodeEnum.NOT_FOUND
                                ).ToDictionary()
                            );
                    }

                    return Results.Ok(
                           ResponseHelper<UserRolesDeleteResponseDto>.Success(
                               message: "Role Deleted Successfully",
                               data: result
                           ).ToDictionary()
                       );
                }
                catch (Exception ex)
                {
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Deleting the User Roles.",
                            exception: ex,
                            isWarning: false,
                            statusCode: StatusCodeEnum.INTERNAL_SERVER_ERROR
                        ).ToDictionary()
                    );

                }
            }).WithTags("User Role")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Deletes a User Role. ", description: "This endpoint allows you to delete a User Role based on the provided User Role Id."
            ));
        }
    }
}
