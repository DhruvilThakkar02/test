using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.User.User.UserResponseDtos;
using HRMS.Dtos.User.UserRoleMapping.UserRoleMappingRequestDtos;
using HRMS.Dtos.User.UserRoleMapping.UserRoleMappingResponseDtos;
using HRMS.Utility.Helpers.Handlers;
using HRMS.Utility.Helpers.Enums;
using HRMS.Utility.Helpers.LogHelpers.Interface;
using HRMS.Utility.Validators.User.UserRoleMapping;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;

namespace HRMS.API.Endpoints.User
{
    public static class UserRolesMappingEndpoints
    {
        public static void MapUserRoleMappingEndpoints(this IEndpointRouteBuilder app)
        {
            /// <summary> 
            /// Retrieves a List of User Roles Mapping. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint returns a List of User Roles Mapping. If no User Roles Mapping are found, a 404 status code is returned. 
            /// </remarks> 
            /// <returns>A List of User Roles Mapping or a 404 status code if no User Roles Mapping are found.</returns>
            app.MapGet("/GetUserRolesMapping", async (IUserRoleMappingService _rolesmappingService, IUserRoleMappingLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(new { _rolesmappingService });
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Fetching all User Roles Mapping.");
                var rolesmapping = await _rolesmappingService.GetUserRolesMapping();
                if (rolesmapping != null && rolesmapping.Any())
                {
                    var response = ResponseHelper<List<UserRoleMappingReadResponseDto>>.Success("User Roles Mapping Retrieved Successfully ", rolesmapping.ToList());
                    logger.LogInformation("Successfully retrieved {Count} rolesmapping.", rolesmapping.Count());
                    return Results.Ok(response.ToDictionary());
                }

                logger.LogWarning("No User Roles Mapping Roles found.");
                var errorResponse = ResponseHelper<List<UserReadResponseDto>>.Error("No User Roles Mapping Roles Found");
                return Results.NotFound(errorResponse.ToDictionary());
            }).WithTags("User Role Mapping")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Retrieves a List of User Roles Mapping", description: "This endpoint returns a List of User Roles Mapping. If no User Roles Mapping are found, a 404 status code is returned."
            ));

            /// <summary> 
            /// Retrieve User Role Mapping by Id. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint return User Role Mapping by Id. If no User Role Mapping are found, a 404 status code is returned. 
            /// </remarks> 
            /// <returns>A User Role Mapping or a 404 status code if no User Role Mapping are found.</returns>
            app.MapGet("/GetUserRoleMappingById/{id}", async (IUserRoleMappingService _rolesmappingService, int id, IUserRoleMappingLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(new { id });
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Fetching User Role Mapping with Id {UserRoleMappingId}.", id);

                var validator = new UserRoleMappingReadRequestValidator();
                var rolesmappingRequestDto = new UserRoleMappingReadRequestDto { UserRoleMappingId = id };

                var validationResult = validator.Validate(rolesmappingRequestDto);
                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for User Roles Mapping with Id {UserRoleMappingId}: {Errors}", id, string.Join(", ", errorMessages));
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
                    var rolemapping = await _rolesmappingService.GetUserRoleMappingById(id);
                    if (rolemapping == null)
                    {
                        logger.LogWarning("User Roles Mapping with Id {UserRoleMappingId} not found.", id);
                        return Results.NotFound(
                            ResponseHelper<string>.Error(
                                message: "User Role Mapping Not Found ",
                                statusCode: StatusCode.NOT_FOUND
                                ).ToDictionary()
                                );
                    }

                    logger.LogInformation("Successfully retrieved User Roles Mapping with Id {UserRoleMappingId}.", id);
                    return Results.Ok(
                        ResponseHelper<UserRoleMappingReadResponseDto>.Success(
                            message: "User Roles Mapping Retrieved Successfully",
                            data: rolemapping
                            ).ToDictionary()
                        );

                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while retrieving the User Roles Mapping with Id {UserRoleMappingId}.", id);
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
            }).WithTags("User Role Mapping")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Retrieve User Role Mapping by Id", description: "This endpoint return User Role Mapping by Id. If no User Role Mapping are found, a 404 status code is returned."
            ));

            /// <summary> 
            /// Creates a new User Role Mapping. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint allows you to create a new User Role Mapping with the provided details. 
            /// </remarks> 
            ///<returns> A success or error response based on the operation result.</returns >
            app.MapPost("/CreateUserRoleMapping", async (UserRoleMappingCreateRequestDto dto, IUserRoleMappingService _rolesmappingService, IUserRoleMappingLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(dto);
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Creating new User Roles Mapping with data: {UserRoleMappingData}", dto);

                var validator = new UserRoleMappingCreateRequestValidator();
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for creating User Roles Mapping: {Errors}", string.Join(", ", errorMessages));
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
                    var newrolemapping = await _rolesmappingService.CreateUserRoleMapping(dto);
                    logger.LogInformation("Successfully created User Role Mapping with Id {UserRoleMappingId}.", newrolemapping.UserRoleMappingId);
                    return Results.Ok(
                        ResponseHelper<UserRoleMappingCreateResponseDto>.Success(
                            message: "User Role Mapping Created Successfully",
                            data: newrolemapping
                        ).ToDictionary()
                    );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while creating the User Role Mapping.");
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
            }).WithTags("User Role Mapping");

            app.MapPut("/UpdateUserRoleMapping", async (IUserRoleMappingService _rolesmappingService, UserRoleMappingUpdateRequestDto dto, IUserRoleMappingLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(dto);
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Updating User Role Mapping with ID {UserRoleMappingId}.", dto.UserRoleMappingId);

                var validator = new UserRoleMappingUpdateRequestValidatorcs();
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for updating User Role Mapping with Id {UserRoleMappingId}: {Errors}", dto.UserRoleMappingId, string.Join(", ", errorMessages));
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
                    var updatedUserMappingRole = await _rolesmappingService.UpdateUserRoleMapping(dto);
                    if (updatedUserMappingRole == null)
                    {
                        logger.LogWarning("User Role Mapping with Id {UserRoleMappingId} not found for update.", dto.UserRoleMappingId);
                        return Results.NotFound(
                            ResponseHelper<string>.Error(
                                message: "User Mapping Roles Not Found",
                                statusCode: StatusCode.NOT_FOUND
                            ).ToDictionary()
                         );
                    }

                    logger.LogInformation("Successfully updated User Role Mapping with Id {UserRoleMappingId}.", dto.UserRoleMappingId);
                    return Results.Ok(
                        ResponseHelper<UserRoleMappingUpdateResponseDto>.Success(
                            message: "User Roles Updated Succesfully ",
                            data: updatedUserMappingRole
                            ).ToDictionary()
                        );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while updating the User Roles Mapping with Id {UserRoleMappingId}.", dto.UserRoleMappingId);
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Updating the User Mapping Roles.",
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
            }).WithTags("User Role Mapping")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Creates a new User Role Mapping.", description: "This endpoint allows you to create a new User Role Mapping with the provided details."
            ));


            /// <summary> 
            /// Deletes a User Roles Mapping . 
            /// </summary> 
            /// <remarks> 
            /// This endpoint allows you to delete a User Roles Mapping based on the provided User Role Mapping Id.</remarks>
            app.MapDelete("/DeleteUserRoleMapping", async (IUserRoleMappingService _rolesmappingService, [FromBody] UserRoleMappingDeleteRequestDto dto, IUserRoleMappingLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(dto);
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Deleting User Roles Mapping with Id {UserRoleMappingId}.", dto.UserRoleMappingId);

                var validator = new UserRoleMappingDeleteRequestValidator();
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for deleting User Role Mapping with Id {UserRoleMappingId}: {Errors}", dto.UserRoleMappingId, string.Join(", ", errorMessages));
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
                    var result = await _rolesmappingService.DeleteUserRoleMapping(dto);
                    if (result == null)
                    {
                        logger.LogWarning("User Role Mapping with ID {UserRoleMappingId} not found for deletion.", dto.UserRoleMappingId);
                        return Results.NotFound(
                            ResponseHelper<string>.Error(
                                message: "User Role Mapping Not Found",
                                statusCode: StatusCode.NOT_FOUND
                                ).ToDictionary()
                            );
                    }

                    logger.LogInformation("Successfully Deleted User Role Mapping with Id {UserRoleMappingId}.", dto.UserRoleMappingId);
                    return Results.Ok(
                           ResponseHelper<UserRoleMappingDeleteResponseDto>.Success(
                               message: "User Role Mapping Deleted Successfully"
                           ).ToDictionary()
                       );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while deleting the User Role Mapping with Id {UserRoleMappingId}.", dto.UserRoleMappingId);
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Deleting User Role Mapping.",
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
            }).WithTags("User Role Mapping")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Deletes a User Role Mapping. ", description: "This endpoint allows you to delete a User Role Mapping based on the provided User Role Mapping Id."
            ));
        }
    }
}
