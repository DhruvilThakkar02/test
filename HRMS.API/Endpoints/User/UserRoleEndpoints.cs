using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.User.User.UserResponseDtos;
using HRMS.Dtos.User.UserRole.UserRoleRequestDtos;
using HRMS.Dtos.User.UserRole.UserRoleResponseDtos;
using HRMS.Utility.Helpers.Enums;
using HRMS.Utility.Helpers.Handlers;
using HRMS.Utility.Validators.User.UserRole;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Newtonsoft.Json;
using HRMS.Utility.Helpers.LogHelpers.Interface;
using Serilog;

namespace HRMS.API.Endpoints.User
{
    public static class UserRoleEndpoints
    {
        public static void MapUserRoleEndpoints(this IEndpointRouteBuilder app)
        {
            /// <summary> 
            /// Retrieves a List of User Roles. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint returns a List of User Roles. If no User Roles are found, a 404 status code is returned. 
            /// </remarks> 
            /// <returns>A List of User Roles or a 404 status code if no User Roles are found.</returns>
            app.MapGet("/GetUserRoles", async (IUserRoleService _rolesService, IUserRoleLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(new { _rolesService });
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Fetching all User Roles.");

                var roles = await _rolesService.GetUserRoles();
                if (roles != null && roles.Any())
                {
                    var response = ResponseHelper<List<UserRoleReadResponseDto>>.Success("User Roles Retrieved Successfully ", roles.ToList());
                    logger.LogInformation("Successfully retrieved {Count} UserRole.", roles.Count());
                    return Results.Ok(response.ToDictionary());
                }

                var errorResponse = ResponseHelper<List<UserReadResponseDto>>.Error("No User Roles Found");
                return Results.NotFound(errorResponse.ToDictionary());
            }).WithTags("User Role")
            .RequireAuthorization("User")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Retrieves a List of User Roles", description: "This endpoint returns a List of User Roles. If no User Roles are found, a 404 status code is returned."
            ));

            /// <summary> 
            /// Retrieve User Role by Id. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint return User Role by Id. If no User Role are found, a 404 status code is returned. 
            /// </remarks> 
            /// <returns>A User Role or a 404 status code if no User Role are found.</returns>
            app.MapGet("/GetUserRoleById/{id}", async (IUserRoleService _rolesService, int id, IUserRoleLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(new { id });
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Fetching User Role with Id {UserRoleId}.", id);

                var validator = new UserRoleReadRequestValidator();
                var rolesRequestDto = new UserRoleReadRequestDto { UserRoleId = id };

                var validationResult = validator.Validate(rolesRequestDto);
                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for UserRole with Id {UserId}: {Errors}", id, string.Join(", ", errorMessages));
                    return Results.BadRequest(
                        ResponseHelper<List<string>>.Error(
                            message: "Validation Failed",
                            errors: errorMessages,
                            statusCode: StatusCode.BAD_REQUEST
                        ).ToDictionary()
                    );
                }
                try
                {
                    var role = await _rolesService.GetUserRoleById(id);
                    if (role == null)
                    {
                        logger.LogWarning("Role with Id {UserRoleId} not found.", id);
                        return Results.NotFound(
                            ResponseHelper<string>.Error(
                                message: "Role Not Found ",
                                statusCode: StatusCode.NOT_FOUND
                                ).ToDictionary()
                                );
                    }

                    logger.LogInformation("Successfully retrieved Role with Id {UserRoleId}.", id);
                    return Results.Ok(
                        ResponseHelper<UserRoleReadResponseDto>.Success(
                            message: "Role Retrieved Successfully",
                            data: role
                            ).ToDictionary()
                        );

                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while retrieving the User Role with Id {UserRoleId}.", id);
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred.",
                            exception: ex,
                            isWarning: false,
                            statusCode: StatusCode.INTERNAL_SERVER_ERROR
                        ).ToDictionary()
                    );
                }
                finally
                {
                    Log.CloseAndFlush();
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
            app.MapPost("/CreateUserRole", async (UserRoleCreateRequestDto dto, IUserRoleService _rolesService, IUserRoleLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(dto);
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Creating new User Role with data: {UserRoleData}", dto);

                var validator = new UserRoleCreateRequestValidator();
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for creating User Roles: {Errors}", string.Join(", ", errorMessages));
                    return Results.BadRequest(
                        ResponseHelper<List<string>>.Error(
                            message: "Validation Failed",
                            errors: errorMessages,
                            statusCode: StatusCode.BAD_REQUEST
                        ).ToDictionary()
                    );
                }
                try
                {
                    var newRole = await _rolesService.CreateUserRole(dto);
                    logger.LogInformation("Successfully created User Role with Id {UserRoleId}.", newRole.UserRoleId);
                    return Results.Ok(
                        ResponseHelper<UserRoleCreateResponseDto>.Success(
                            message: "Role Created Successfully",
                            data: newRole
                        ).ToDictionary()
                    );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while creating the Role.");
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Creating the Role.",
                            exception: ex,
                            isWarning: false,
                            statusCode: StatusCode.INTERNAL_SERVER_ERROR
                        ).ToDictionary()
                    );
                }
                finally
                {
                    Log.CloseAndFlush();
                }
            }).WithTags("User Role")
            .AllowAnonymous()
            .WithMetadata(new SwaggerOperationAttribute(summary: "Creates a new User Role.", description: "This endpoint allows you to create a new User Role with the provided details."
            ));

            /// <summary> 
            /// Updates existing User Role details. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint allows you to update User Role details with the provided Id. 
            /// </remarks> 
            ///<returns> A success or error response based on the operation result.</returns >
            app.MapPut("/UpdateUserRole", async (IUserRoleService _rolesService, [FromBody] UserRoleUpdateRequestDto dto, IUserRoleLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(dto);
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Updating User Role with ID {UserRoleId}.", dto.UserRoleId);

                var validator = new UserRoleUpdateRequestValidator();
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for updating User Role with Id {UserRoleId}: {Errors}", dto.UserRoleId, string.Join(", ", errorMessages));
                    return Results.BadRequest(
                       ResponseHelper<List<string>>.Error(
                           message: "Validation Failed",
                           errors: errorMessages,
                           statusCode: StatusCode.BAD_REQUEST
                       ).ToDictionary()
                   );
                }

                try
                {
                    var updatedUserRole = await _rolesService.UpdateUserRole(dto);
                    if (updatedUserRole == null)
                    {
                        logger.LogWarning("User Role with Id {UserRoleId} not found for update.", dto.UserRoleId);
                        return Results.NotFound(
                            ResponseHelper<string>.Error(
                                message: "User Roles Not Found",
                                statusCode: StatusCode.NOT_FOUND
                            ).ToDictionary()
                         );
                    }

                    logger.LogInformation("Successfully updated User Role with Id {UserRoleId}.", dto.UserRoleId);
                    return Results.Ok(
                        ResponseHelper<UserRoleUpdateResponseDto>.Success(
                            message: "User Roles Updated Succesfully ",
                            data: updatedUserRole
                            ).ToDictionary()
                        );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while updating the User Role with Id {UserRoleId}.", dto.UserRoleId);
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Updating the User Roles.",
                            exception: ex,
                            isWarning: false,
                            statusCode: StatusCode.INTERNAL_SERVER_ERROR
                        ).ToDictionary()
                    );

                }
                finally
                {
                    Log.CloseAndFlush();
                }

            }).WithTags("User Role")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Updates existing User Role details", description: "This endpoint allows you to update User Role details with the provided Id."
            ));

            /// <summary> 
            /// Deletes a User Role. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint allows you to delete a User Role based on the provided User Role Id.</remarks>
            app.MapDelete("/DeleteUserRole", async (IUserRoleService _rolesService, [FromBody] UserRoleDeleteRequestDto dto, IUserRoleLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(dto);
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Deleting User Role with Id {CompanyBranchId}.", dto.UserRoleId);

                var validator = new UserRoleDeleteRequestValidator();
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for deleting UserRole with Id {UserRoleId}: {Errors}", dto.UserRoleId, string.Join(", ", errorMessages));
                    return Results.BadRequest(
                      ResponseHelper<List<string>>.Error(
                          message: "Validation Failed",
                          errors: errorMessages,
                          statusCode: StatusCode.BAD_REQUEST
                      ).ToDictionary()
                  );
                }
                try
                {
                    var result = await _rolesService.DeleteUserRole(dto);
                    if (result == null)
                    {
                        logger.LogWarning("User Role with ID {UserRoleId} not found for deletion.", dto.UserRoleId);
                        return Results.NotFound(
                            ResponseHelper<string>.Error(
                                message: "User Roles Not Found",
                                statusCode: StatusCode.NOT_FOUND
                                ).ToDictionary()
                            );
                    }

                    logger.LogInformation("Successfully Deleted UserRole with Id {UserRoleId}.", dto.UserRoleId);
                    return Results.Ok(
                           ResponseHelper<UserRoleDeleteResponseDto>.Success(
                               message: "Role Deleted Successfully"
                           ).ToDictionary()
                       );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while deleting the UserRole with Id {UserRoleId}.", dto.UserRoleId);
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Deleting the User Roles.",
                            exception: ex,
                            isWarning: false,
                            statusCode: StatusCode.INTERNAL_SERVER_ERROR
                        ).ToDictionary()
                    );

                }
                finally
                {
                    Log.CloseAndFlush();
                }
            }).WithTags("User Role")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Deletes a User Role. ", description: "This endpoint allows you to delete a User Role based on the provided User Role Id."
            ));
        }
    }
}
