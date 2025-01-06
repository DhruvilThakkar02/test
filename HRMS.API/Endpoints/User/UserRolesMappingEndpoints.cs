using FluentValidation;
using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.User.User.UserResponseDtos;
using HRMS.Dtos.User.UserRoles.UserRolesRequestDtos;
using HRMS.Dtos.User.UserRolesMapping.UserRolesMappingRequestDtos;
using HRMS.Dtos.User.UserRolesMapping.UserRolesMappingResponseDtos;
using HRMS.Utility.Helpers.Enums;
using HRMS.Utility.Helpers.Handlers;
using HRMS.Utility.Validators.User.UserRoles;
using HRMS.Utility.Validators.User.UserRolesMapping;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HRMS.API.Endpoints.User
{
    public static class UserRolesMappingEndpoints
    {
        public static void MapUserRolesMappingEndpoints(this IEndpointRouteBuilder app)
        {
            /// <summary> 
            /// Retrieves a List of User Roles Mapping. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint returns a List of User Roles Mapping. If no User Roles Mapping are found, a 404 status code is returned. 
            /// </remarks> 
            /// <returns>A List of User Roles Mapping or a 404 status code if no User Roles Mapping are found.</returns>
            app.MapGet("/GetUserRolesMapping", async (IUserRoleMappingService _rolesmappingService) =>
            {
                var rolesmapping = await _rolesmappingService.GetUserRolesMapping();
                if (rolesmapping != null && rolesmapping.Any())
                {
                    var response = ResponseHelper<List<UserRoleMappingReadResponseDto>>.Success("User Roles Mapping Retrieved Successfully ", rolesmapping.ToList());
                    return Results.Ok(response.ToDictionary());
                }

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
            app.MapGet("/GetUserRoleMappingById/{id}", async (IUserRoleMappingService _rolesmappingService, int id) =>
            {
                var validator = new UserRoleMappingReadRequestValidator();
                var rolesmappingRequestDto = new UserRoleMappingReadRequestDto { UserRoleMappingId = id };

                var validationResult = validator.Validate(rolesmappingRequestDto);
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
                    var rolemapping = await _rolesmappingService.GetUserRoleMappingById(id);
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
                        ResponseHelper<UserRoleMappingReadResponseDto>.Success(
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
            app.MapPost("/CreateUserRoleMapping", async (UserRoleMappingCreateRequestDto dto, IUserRoleMappingService _rolesmappingService) =>
            {
                var validator = new UserRoleMappingCreateRequestValidator();
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
                    var newrolemapping = await _rolesmappingService.CreateUserRoleMapping(dto);
                    return Results.Ok(
                        ResponseHelper<UserRoleMappingCreateResponseDto>.Success(
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
            }).WithTags("User Role Mapping");

            app.MapPut("/UpdateUserRoleMapping", async (IUserRoleMappingService _rolesmappingService, UserRoleMappingUpdateRequestDto dto) =>
            {
                var validator = new UserRoleMappingUpdateRequestValidatorcs();
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
                        ResponseHelper<UserRoleMappingUpdateResponseDto>.Success(
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
            }).WithTags("User Role Mapping")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Creates a new User Role Mapping.", description: "This endpoint allows you to create a new User Role Mapping with the provided details."
            ));


            /// <summary> 
            /// Deletes a User Roles Mapping . 
            /// </summary> 
            /// <remarks> 
            /// This endpoint allows you to delete a User Roles Mapping based on the provided User Role Mapping Id.</remarks>
            app.MapDelete("/DeleteUserRoleMapping", async (IUserRoleMappingService _rolesmappingService,[FromBody] UserRoleMappingDeleteRequestDto dto) =>
            {
                var validator = new UserRoleMappingDeleteRequestValidator();
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
                    var result = await _rolesmappingService.DeleteUserRoleMapping(dto);
                    if (result == null)
                    {
                        return Results.NotFound(
                            ResponseHelper<string>.Error(
                                message: "User Role Mapping Not Found",
                                statusCode: StatusCodeEnum.NOT_FOUND
                                ).ToDictionary()
                            );
                    }

                    return Results.Ok(
                           ResponseHelper<UserRoleMappingDeleteResponseDto>.Success(
                               message: "User Role Mapping Deleted Successfully"
                           ).ToDictionary()
                       );
                }
                catch (Exception ex)
                {
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Deleting User Role Mapping.",
                            exception: ex,
                            isWarning: false,
                            statusCode: StatusCodeEnum.INTERNAL_SERVER_ERROR
                        ).ToDictionary()
                    );

                }
            }).WithTags("User Role Mapping")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Deletes a User Role Mapping. ", description: "This endpoint allows you to delete a User Role Mapping based on the provided User Role Mapping Id."
            ));
        }
    }
}
