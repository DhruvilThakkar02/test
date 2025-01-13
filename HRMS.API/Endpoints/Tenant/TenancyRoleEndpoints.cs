using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.Tenant.TenancyRole.TenancyRoleRequestDtos;
using HRMS.Dtos.Tenant.TenancyRole.TenancyRoleResponseDtos;
using HRMS.Utility.Helpers.Enums;
using HRMS.Utility.Helpers.Handlers;
using HRMS.Utility.Validators.Tenant.TenancyRole;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using HRMS.Utility.Helpers.LogHelpers.Interface;
using Swashbuckle.AspNetCore.Annotations;
using Serilog;

namespace HRMS.API.Endpoints.Tenant
{
    public static class TenancyRoleEndpoints
    {
        public static void MapTenancyRoleEndpoints(this IEndpointRouteBuilder app)
        {
            /// <summary> 
            /// Retrieves a List of Tenancy Roles. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint returns a List of Tenancy Roles. If no Tenancy Roles are found, a 404 status code is returned. 
            /// </remarks> 
            /// <returns>A List of Tenancy Roles or a 404 status code if no Tenancy Roles are found.</returns>
            app.MapGet("/tenancyrole/getall", async (ITenancyRoleService service, ITenancyRoleLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(new { service });
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Fetching all Tenancy Roles.");

                var roles = await service.GetTenancyRoles();
                if (roles != null && roles.Any())
                {
                    var response = ResponseHelper<List<TenancyRoleReadResponseDto>>.Success("Tenancy Roles Retrieved Successfully", roles.ToList());
                    logger.LogInformation("Successfully retrieved {Count} TenancyRole.", roles.Count());
                    return Results.Ok(response.ToDictionary());
                }

                logger.LogWarning("No Tenancy Roles found.");
                var errorResponse = ResponseHelper<List<TenancyRoleReadResponseDto>>.Error("No Tenancy Roles Found");
                return Results.NotFound(errorResponse.ToDictionary());
            }).WithTags("Tenancy Role")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Retrieves a List of Tenancy Roles", description: "This endpoint returns a List of Tenancy Roles. If no Tenancy Roles are found, a 404 status code is returned."
            ));

            /// <summary> 
            /// Retrieve Tenancy Role by Id. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint return Tenancy Role by Id. If no Tenancy Role are found, a 404 status code is returned. 
            /// </remarks> 
            /// <returns>A Tenancy Role or a 404 status code if no Tenancy Role are found.</returns>
            app.MapGet("/tenancyRole/{id}", async (ITenancyRoleService service, int id, ITenancyRoleLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(new { id });
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Fetching TenancyRole with Id {TenancyRoleId}.", id);

                var validator = new TenancyRoleReadRequestValidator();
                var roleRequestDto = new TenancyRoleReadRequestDto { TenancyRoleId = id };

                var validationResult = validator.Validate(roleRequestDto);
                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for TenanancyRole with Id {TenancyRoleId}: {Errors}", id, string.Join(", ", errorMessages));
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
                    var role = await service.GetTenancyRoleById(id);
                    if (role == null)
                    {
                        logger.LogWarning("TenancyRole with Id {TenancyRoleId} not found.", id);
                        return Results.NotFound(
                            ResponseHelper<string>.Error(
                                message: "Tenancy Role Not Found",
                                statusCode: StatusCode.NOT_FOUND
                            ).ToDictionary()
                        );
                    }
                    logger.LogInformation("Successfully retrieved TenancyRole with Id {TenancyRoleId}.", id);
                    return Results.Ok(
                        ResponseHelper<TenancyRoleReadResponseDto>.Success(
                            message: "Tenancy Role Retrieved Successfully",
                            data: role
                        ).ToDictionary()
                    );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while retrieving the TenancyRole with Id {TenancyRoleId}.", id);
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

            }).WithTags("Tenancy Role")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Retrieve Tenancy Role by Id", description: "This endpoint return Tenancy Role by Id. If no Tenancy Role are found, a 404 status code is returned."
            ));

            /// <summary> 
            /// Creates a new Tenancy Role. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint allows you to create a new Tenancy Role with the provided details. 
            /// </remarks> 
            ///<returns> A success or error response based on the operation result.</returns >
            app.MapPost("/tenancyrole/create", async (TenancyRoleCreateRequestDto dto, ITenancyRoleService _tenancyroleService, ITenancyRoleLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(dto);
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Creating new TenancyRole with data: {TenancyRoleData}", dto);
                var validator = new TenancyRoleCreateRequestValidator();
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for creating TenancyRole: {Errors}", string.Join(", ", errorMessages));
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
                    var newRole = await _tenancyroleService.CreateTenancyRole(dto);
                    logger.LogInformation("Successfully created TenancyRole with Id {TenancyRoleId}.", newRole.TenancyRoleId);
                    return Results.Ok(
                        ResponseHelper<TenancyRoleCreateResponseDto>.Success(
                            message: "Tenancy Role Created Successfully",
                            data: newRole
                        ).ToDictionary()
                    );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while creating the TenancyRole.");
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Creating the Tenancy Role.",
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
            }).WithTags("Tenancy Role")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Creates a new Tenancy Role.", description: "This endpoint allows you to create a new Tenancy Role with the provided details."
            ));

            /// <summary> 
            /// Updates existing Tenancy Role details. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint allows you to update Tenancy Role details with the provided Id. 
            /// </remarks> 
            ///<returns> A success or error response based on the operation result.</returns >
            app.MapPut("/tenancyrole/update", async (ITenancyRoleService service, [FromBody] TenancyRoleUpdateRequestDto dto, ITenancyRoleLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(dto);
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Updating TenancyRole with ID {TenancyRoleId}.", dto.TenancyRoleId);

                var validator = new TenancyRoleUpdateRequestValidator();
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for updating TenancyRole with Id {TenancyRoleId}: {Errors}", dto.TenancyRoleId, string.Join(", ", errorMessages));
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
                    var updatedTenancyRole = await service.UpdateTenancyRole(dto);
                    if (updatedTenancyRole == null)
                    {
                        logger.LogWarning("TenancyRole with Id {TenancyRoleId} not found for update.", dto.TenancyRoleId);
                        return Results.NotFound(
                           ResponseHelper<string>.Error(
                               message: "Tenancy Role Not Found",
                               statusCode: StatusCode.NOT_FOUND
                           ).ToDictionary()
                       );
                    }

                    logger.LogInformation("Successfully updated TenancyRole with Id {TenancyRoleId}.", dto.TenancyRoleId);
                    return Results.Ok(
                        ResponseHelper<TenancyRoleUpdateResponseDto>.Success(
                            message: "Tenancy Role Updated Successfully",
                            data: updatedTenancyRole
                        ).ToDictionary()
                    );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while updating the TenancyRole with Id {UserId}.", dto.TenancyRoleId);
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Updating the Tenancy Role.",
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
            }).WithTags("Tenancy Role")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Updates existing Tenancy Role details", description: "This endpoint allows you to update Tenancy Role details with the provided Id."
            ));

            /// <summary> 
            /// Deletes a Tenancy Role. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint allows you to delete a Tenancy Role based on the provided Tenancy Role Id.</remarks>
            app.MapDelete("/tenancyrole/delete", async (ITenancyRoleService service, [FromBody] TenancyRoleDeleteRequestDto dto, ITenancyRoleLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(dto);
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Deleting TenancyRole with ID {TenancyRoleId}.", dto.TenancyRoleId);

                var validator = new TenancyRoleDeleteRequestValidator();
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for Deleting TenancyRole with Id {TenancyRoleId}: {Errors}", dto.TenancyRoleId, string.Join(", ", errorMessages));
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
                    var result = await service.DeleteTenancyRole(dto);
                    if (result == null)
                    {
                        logger.LogWarning("TenancyRole with Id {TenancyRoleId} not found for Delete.", dto.TenancyRoleId);
                        return Results.NotFound(
                           ResponseHelper<string>.Error(
                               message: "Tenancy Role Not Found",
                               statusCode: StatusCode.NOT_FOUND
                           ).ToDictionary()
                       );
                    }

                    logger.LogInformation("Successfully Delete TenancyRole with Id {TenancyRoleId}.", dto.TenancyRoleId);
                    return Results.Ok(
                       ResponseHelper<TenancyRoleDeleteResponseDto>.Success(
                           message: "Tenancy Role Deleted Successfully"
                       ).ToDictionary()
                   );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while Deleting the TenancyRole with Id {TenancyRoleId}.", dto.TenancyRoleId);
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Deleting the Tenancy Role.",
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
            }).WithTags("Tenancy Role")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Deletes a Tenancy Role. ", description: "This endpoint allows you to delete a Tenancy Role based on the provided Tenancy Role Id."
            ));
        }
    }
}
