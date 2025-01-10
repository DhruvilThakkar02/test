﻿using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.User.User.UserRequestDtos;
using HRMS.Dtos.User.User.UserResponseDtos;
using HRMS.Utility.Helpers.Enums;
using HRMS.Utility.Helpers.Handlers;
using HRMS.Utility.Helpers.LogHelpers.Interface;
using HRMS.Utility.Validators.User.User;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;

namespace HRMS.API.Modules.User
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            /// <summary> 
            /// Retrieves a List of Users. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint returns a List of Users. If no Users are found, a 404 status code is returned. 
            /// </remarks> 
            /// <returns>A List of Users or a 404 status code if no Users are found.</returns>
            app.MapGet("/GetUsers", async (IUserService service, IUserLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(new { service });
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Fetching all Users.");

                var users = await service.GetUsers();
                if (users != null && users.Any())
                {
                    var response = ResponseHelper<List<UserReadResponseDto>>.Success("Users Retrieved Successfully", users.ToList());
                    logger.LogInformation("Successfully retrieved {Count} Users.", users.Count());
                    return Results.Ok(response.ToDictionary());
                }

                logger.LogWarning("No Users found.");
                var errorResponse = ResponseHelper<List<UserReadResponseDto>>.Error("No Users Found");
                return Results.NotFound(errorResponse.ToDictionary());
            })
            .WithTags("User")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Retrieves a List of Users", description: "This endpoint returns a List of Users. If no Users are found, a 404 status code is returned."
            ));

            

            /// <summary> 
            /// Retrieve User by Id. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint return User by Id. If no User are found, a 404 status code is returned. 
            /// </remarks> 
            /// <returns>A User or a 404 status code if no User are found.</returns>
            app.MapGet("/GetUserById/{id}", async (IUserService service, int id, IUserLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(new { id });
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Fetching User with Id {UserId}.", id);

                var validator = new UserReadRequestValidator();
                var userRequestDto = new UserReadRequestDto { UserId = id };

                var validationResult = validator.Validate(userRequestDto);
                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for User with Id {UserId}: {Errors}", id, string.Join(", ", errorMessages));
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
                    var user = await service.GetUserById(id);
                    if (user == null)
                    {
                        return Results.NotFound(
                            ResponseHelper<string>.Error(
                                message: "User Not Found",
                                statusCode: StatusCode.NOT_FOUND
                            ).ToDictionary()
                        );
                    }

                    logger.LogInformation("Successfully retrieved User with Id {UserId}.", id);
                    return Results.Ok(
                        ResponseHelper<UserReadResponseDto>.Success(
                            message: "User Retrieved Successfully",
                            data: user
                        ).ToDictionary()
                    );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while retrieving the User with Id {UserId}.", id);
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
            }).WithTags("User")
            .RequireAuthorization("AdminOnly")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Retrieve User by Id", description: "This endpoint return User by Id. If no User are found, a 404 status code is returned."
            ));

            /// <summary> 
            /// Creates a new User. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint allows you to create a new User with the provided details. 
            /// </remarks> 
            ///<returns> A success or error response based on the operation result.</returns >
            app.MapPost("/CreateUser", async (UserCreateRequestDto dto, IUserService _userService, IUserLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(dto);
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Creating new User with data: {UserData}", dto);

                var validator = new UserCreateRequestValidator();
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for creating User: {Errors}", string.Join(", ", errorMessages));
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
                    var newUser = await _userService.CreateUser(dto);
                    logger.LogInformation("Successfully created User with Id {UserId}.", newUser.UserId);
                    return Results.Ok(
                        ResponseHelper<UserCreateResponseDto>.Success(
                            message: "User Created Successfully",
                            data: newUser
                        ).ToDictionary()
                    );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while creating the User.");
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Creating the User.",
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
            }).WithTags("User")
            .AllowAnonymous()
            .WithMetadata(new SwaggerOperationAttribute(summary: "Creates a new User.", description: "This endpoint allows you to create a new User with the provided details."
            ));

            /// <summary> 
            /// Updates existing User details. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint allows you to update User details with the provided Id. 
            /// </remarks> 
            ///<returns> A success or error response based on the operation result.</returns >
            app.MapPut("/UpdateUser", async (IUserService service, [FromBody] UserUpdateRequestDto dto, IUserLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(dto);
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Updating User with ID {UserId}.", dto.UserId);

                var validator = new UserUpdateRequestValidator();
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for updating User with Id {UserId}: {Errors}", dto.UserId, string.Join(", ", errorMessages));
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
                    var updatedUser = await service.UpdateUser(dto);
                    if (updatedUser == null)
                    {
                        logger.LogWarning("User with Id {UserId} not found for update.", dto.UserId);
                        return Results.NotFound(
                           ResponseHelper<string>.Error(
                               message: "User Not Found",
                               statusCode: StatusCode.NOT_FOUND
                           ).ToDictionary()
                       );
                    }

                    logger.LogInformation("Successfully updated User with Id {UserId}.", dto.UserId);
                    return Results.Ok(
                        ResponseHelper<UserUpdateResponseDto>.Success(
                            message: "User Updated Successfully",
                            data: updatedUser
                        ).ToDictionary()
                    );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while updating the UserRole with Id {UserId}.", dto.UserId);
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Updating the User.",
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
            }).WithTags("User")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Updates existing User details", description: "This endpoint allows you to update User details with the provided Id."
            ));

            /// <summary> 
            /// Deletes a User. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint allows you to delete a User based on the provided User Id.</remarks>
            app.MapDelete("/DeleteUser", async (IUserService service, [FromBody] UserDeleteRequestDto dto, IUserLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(dto);
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Deleting User with ID {UserId}.", dto.UserId);

                var validator = new UserDeleteRequestValidator();
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for updating User with Id {UserId}: {Errors}", dto.UserId, string.Join(", ", errorMessages));
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
                    var result = await service.DeleteUser(dto);
                    logger.LogWarning("User with Id {UserId} not found for Delete.", dto.UserId);
                    if (result == null)
                    {
                        return Results.NotFound(
                           ResponseHelper<string>.Error(
                               message: "User Not Found",
                               statusCode: StatusCode.NOT_FOUND
                           ).ToDictionary()
                       );
                    }

                    logger.LogInformation("Successfully Deleted User with Id {UserId}.", dto.UserId);
                    return Results.Ok(
                       ResponseHelper<UserDeleteResponseDto>.Success(
                           message: "User Deleted Successfully"
                       ).ToDictionary()
                   );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while Deleting the User with Id {UserId}.", dto.UserId);
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Deleting the User.",
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
            }).WithTags("User")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Deletes a User. ", description: "This endpoint allows you to delete a User based on the provided User Id."
            ));
        }
    }
}
