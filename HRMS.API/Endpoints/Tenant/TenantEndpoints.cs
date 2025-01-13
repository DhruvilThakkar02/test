using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.Tenant.Tenant.TenantRequestDtos;
using HRMS.Dtos.Tenant.Tenant.TenantResponseDtos;
using HRMS.Utility.Validators.Tenant.Tenant;
using Microsoft.AspNetCore.Mvc;
using HRMS.Utility.Helpers.Handlers;
using Swashbuckle.AspNetCore.Annotations;
using HRMS.Utility.Helpers.Enums;
using HRMS.Utility.Helpers.LogHelpers.Interface;
using Newtonsoft.Json;
using Serilog;

namespace HRMS.API.Endpoints.Tenant
{
    public static class TenantEndpoints
    {
        public static void MapTenantEndpoints(this IEndpointRouteBuilder app)
        {
            /// <summary> 
            /// Retrieves a List of Tenants. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint returns a List of Tenants. If no Tenants are found, a 404 status code is returned. 
            /// </remarks> 
            /// <returns>A List of Tenants or a 404 status code if no Tenants are found.</returns>
            app.MapGet("/tenant/getall", async (ITenantService service, ITenantLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(new { service });
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Fetching all Tenants.");

                var tenant = await service.GetTenants();
                if (tenant != null && tenant.Any())
                {
                    var response = ResponseHelper<List<TenantReadResponseDtos>>.Success("Tenants Retrieved Successfully", tenant.ToList());
                    logger.LogInformation("Successfully retrieved {Count} Tenants.", tenant.Count());
                    return Results.Ok(response.ToDictionary());
                }

                logger.LogWarning("No Tenants found.");
                var errorResponse = ResponseHelper<List<TenantReadResponseDtos>>.Error("No Tenants Found");
                return Results.NotFound(errorResponse.ToDictionary());
            }).WithTags("Tenant")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Retrieves a List of Tenants", description: "This endpoint returns a List of Tenants. If no Tenants are found, a 404 status code is returned."
            ));

            /// <summary> 
            /// Retrieve Tenant by Id. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint return Tenant by Id. If no Tenant are found, a 404 status code is returned. 
            /// </remarks> 
            /// <returns>A Tenant or a 404 status code if no Tenant are found.</returns>
            app.MapGet("/tenant/{id}", async (ITenantService service, int id, ITenantLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(new { id });
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Fetching Tenant with Id {TenantId}.", id);

                var validator = new TenantReadRequestValidator();
                var tenantRequestDto = new TenantReadRequestDtos { TenantId = id };

                var validationResult = validator.Validate(tenantRequestDto);
                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for Tenant with Id {TenantId}: {Errors}", id, string.Join(", ", errorMessages));
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
                    var tenant = await service.GetTenantById(id);
                    if (tenant == null)
                    {
                        logger.LogWarning("Tenant with Id {TenantId} not found.", id);
                        return Results.NotFound(
                            ResponseHelper<string>.Error(
                                message: "Tenant Not Found",
                                statusCode: StatusCode.NOT_FOUND
                            ).ToDictionary()
                        );
                    }

                    logger.LogInformation("Successfully retrieved Tenant with Id {TenantId}.", id);
                    return Results.Ok(
                        ResponseHelper<TenantReadResponseDtos>.Success(
                            message: "Tenant Retrieved Successfully",
                            data: tenant
                        ).ToDictionary()
                    );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while retrieving the Tenant with Id {TenantId}.", id);
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
            }).WithTags("Tenant")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Retrieve Tenant by Id", description: "This endpoint return Tenant by Id. If no Tenant are found, a 404 status code is returned."
            ));

            /// <summary> 
            /// Creates a new Tenant. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint allows you to create a new Tenant with the provided details. 
            /// </remarks> 
            ///<returns> A success or error response based on the operation result.</returns >
            app.MapPost("/tenant/create", async (TenantCreateRequestDtos dto, ITenantService _service, ITenantLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(dto);
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Creating new Tenant with data: {TenantData}", dto);

                var validator = new TenantCreateRequestValidator();
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for creating Tenant: {Errors}", string.Join(", ", errorMessages));
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
                    var newUser = await _service.CreateTenant(dto);
                    logger.LogInformation("Successfully created Tenant with Id {TenantId}.", newUser.TenantId);
                    return Results.Ok(
                        ResponseHelper<TenantCreateResponseDtos>.Success(
                            message: "Tenant Created Successfully",
                            data: newUser
                        ).ToDictionary()
                    );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while creating the Tenant.");
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Creating the Tenant.",
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
            }).WithTags("Tenant")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Creates a new Tenant.", description: "This endpoint allows you to create a new Tenant with the provided details."
            ));

            /// <summary> 
            /// Updates existing Tenant details. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint allows you to update Tenant details with the provided Id. 
            /// </remarks> 
            ///<returns> A success or error response based on the operation result.</returns >
            app.MapPut("/tenant/update", async (ITenantService _service, [FromBody] TenantUpdateRequestDtos dto, ITenantLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(dto);
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Updating Tenant with ID {TenantId}.", dto.TenantId);

                var validator = new TenantUpdateRequestValidator();
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for updating Tenant with Id {TenantId}: {Errors}", dto.TenantId, string.Join(", ", errorMessages));
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
                    var updatedTenant = await _service.UpdateTenant(dto);
                    if (updatedTenant == null)
                    {
                        logger.LogWarning("Tenant with Id {TenantId} not found for update.", dto.TenantId);
                        return Results.NotFound(
                           ResponseHelper<string>.Error(
                               message: "Tenant Not Found",
                               statusCode: StatusCode.NOT_FOUND
                           ).ToDictionary()
                       );
                    }

                    logger.LogInformation("Successfully updated Tenant with Id {TenantId}.", dto.TenantId);
                    return Results.Ok(
                        ResponseHelper<TenantUpdateResponseDtos>.Success(
                            message: "Tenant Updated Successfully",
                            data: updatedTenant
                        ).ToDictionary()
                    );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while updating the Tenant with Id {TenantId}.", dto.TenantId);
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Updating the Tenant.",
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
            }).WithTags("Tenant")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Updates existing Tenant details", description: "This endpoint allows you to update Tenant details with the provided Id."
            ));

            /// <summary> 
            /// Deletes a Tenant. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint allows you to delete a Tenant based on the provided Tenant Id.</remarks>
            app.MapDelete("/tenant/delete", async (ITenantService service, [FromBody] TenantDeleteRequestDtos dto, ITenantLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(dto);
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Deleting Tenant with Id {TenantId}.", dto.TenantId);

                var validator = new TenantDeleteRequestValidator();
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for deleting Tenant with Id {TenantId}: {Errors}", dto.TenantId, string.Join(", ", errorMessages));
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
                    var result = await service.DeleteTenant(dto);
                    if (result == null)
                    {
                        logger.LogWarning("Tenant with ID {TenantId} not found for deletion.", dto.TenantId);
                        return Results.NotFound(
                           ResponseHelper<string>.Error(
                               message: "Tenant Not Found",
                               statusCode: StatusCode.NOT_FOUND
                           ).ToDictionary()
                       );
                    }

                    logger.LogInformation("Successfully Deleted Tenant with Id {TenantId}.", dto.TenantId);
                    return Results.Ok(
                       ResponseHelper<TenantDeleteResponseDtos>.Success(
                           message: "Tenant Deleted Successfully"
                       ).ToDictionary()
                   );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while deleting the Tenant with Id {TenantId}.", dto.TenantId);
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Deleting the Tenant.",
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
            }).WithTags("Tenant")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Deletes a Tenant. ", description: "This endpoint allows you to delete a Tenant based on the provided Tenant Id."
            ));
        }
    }
}
