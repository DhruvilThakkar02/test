using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.Tenant.Organization.OrganizationRequestDtos;
using HRMS.Dtos.Tenant.Organization.OrganizationResponseDtos;
using HRMS.Utility.Helpers.Enums;
using HRMS.Utility.Helpers.Handlers;
using HRMS.Utility.Helpers.LogHelpers.Interface;
using HRMS.Utility.Validators.Tenant.Organization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;

namespace HRMS.API.Endpoints.Tenant
{
    public static class OrganizationEndpoints
    {
        public static void MapOrganizationEndpoints(this IEndpointRouteBuilder app)
        {
            /// <summary> 
            /// Retrieves a List of Organizations. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint returns a List of Organizations. If no Organizations are found, a 404 status code is returned. 
            /// </remarks> 
            /// <returns>A List of Organizations or a 404 status code if no Organizations are found.</returns>
            app.MapGet("/GetOrganizations", async (IOrganizationService service, IOrganizationLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(new { service });
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Fetching all Organizations.");

                var organizations = await service.GetOrganizations();
                if (organizations != null && organizations.Any())
                {
                    var response = ResponseHelper<List<OrganizationReadResponseDto>>.Success("Organizations Retrieved Successfully", organizations.ToList());
                    logger.LogInformation("Successfully retrieved {Count} Organizations.", organizations.Count);
                    return Results.Ok(response.ToDictionary());
                }

                logger.LogWarning("No Organizations found.");
                var errorResponse = ResponseHelper<IEnumerable<OrganizationReadResponseDto>>.Error("No Organizations Found");
                return Results.NotFound(errorResponse.ToDictionary());
            }).WithTags("Organization")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Retrieves a List of Organizations", description: "This endpoint returns a List of Organizations. If no Organizations are found, a 404 status code is returned."
            ));

            /// <summary> 
            /// Retrieve Organization by Id. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint return Organization by Id. If no Organization are found, a 404 status code is returned. 
            /// </remarks> 
            /// <returns>A Organization or a 404 status code if no Organization are found.</returns>
            app.MapGet("/GetOrganizationById/{id}", async (IOrganizationService service, int id, IOrganizationLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(new { id });
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Fetching Organization with Id {OrganizationId}.", id);

                var validator = new OrganizationReadRequestValidator();
                var organizationRequestDto = new OrganizationReadRequestDto { OrganizationId = id };

                var validationResult = validator.Validate(organizationRequestDto);
                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for Organization with Id {OrganizationId}: {Errors}", id, string.Join(", ", errorMessages));
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
                    var organization = await service.GetOrganizationById(id);
                    if (organization == null)
                    {
                        logger.LogWarning("Organization with Id {OrganizationId} not found.", id);
                        return Results.NotFound(
                            ResponseHelper<string>.Error(
                                message: "Organization Not Found",
                                statusCode: StatusCodeEnum.NOT_FOUND
                            ).ToDictionary()
                        );
                    }

                    logger.LogInformation("Successfully retrieved Organization with Id {OrganizationId}.", id);
                    return Results.Ok(
                        ResponseHelper<OrganizationReadResponseDto>.Success(
                            message: "Organization Retrieved Successfully",
                            data: organization
                        ).ToDictionary()
                    );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while retrieving the Organization with Id {OrganizationId}.", id);
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred.",
                            exception: ex,
                            isWarning: false,
                            statusCode: StatusCodeEnum.INTERNAL_SERVER_ERROR
                        ).ToDictionary()
                    );
                }
                finally
                {
                    Log.CloseAndFlush();
                }
            }).WithTags("Organization")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Retrieve Organization by Id", description: "This endpoint return Organization by Id. If no Organization are found, a 404 status code is returned."
            ));

            /// <summary> 
            /// Creates a new Organization. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint allows you to create a new Organization with the provided details. 
            /// </remarks> 
            ///<returns> A success or error response based on the operation result.</returns >
            app.MapPost("/CreateOrganization", async (OrganizationCreateRequestDto dto, IOrganizationService _organizationService, IOrganizationLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(dto);
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Creating new Organization with data: {OrganizationData}", dto);

                var validator = new OrganizationCreateRequestValidator();
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for creating organization: {Errors}", string.Join(", ", errorMessages));
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
                    var newOrganization = await _organizationService.CreateOrganization(dto);
                    logger.LogInformation("Successfully created Organization with Id {OrganizationId}.", newOrganization.OrganizationId);
                    return Results.Ok(
                        ResponseHelper<OrganizationCreateResponseDto>.Success(
                            message: "Organization Created Successfully",
                            data: newOrganization
                        ).ToDictionary()
                    );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while creating the Organization.");
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Creating the Organization.",
                            exception: ex,
                            isWarning: false,
                            statusCode: StatusCodeEnum.INTERNAL_SERVER_ERROR
                        ).ToDictionary()
                    );
                }
                finally
                {
                    Log.CloseAndFlush();
                }
            }).WithTags("Organization")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Creates a new Organization.", description: "This endpoint allows you to create a new Organization with the provided details."
            ));

            /// <summary> 
            /// Updates existing Organization details. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint allows you to update Organization details with the provided Id. 
            /// </remarks> 
            ///<returns> A success or error response based on the operation result.</returns >
            app.MapPut("/UpdateOrganization", async (IOrganizationService service, [FromBody] OrganizationUpdateRequestDto dto, IOrganizationLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(dto);
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Updating Organization with ID {OrganizationId}.", dto.OrganizationId);

                var validator = new OrganizationUpdateRequestValidator();
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for updating Organization with Id {OrganizationId}: {Errors}", dto.OrganizationId, string.Join(", ", errorMessages));
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
                    var updatedOrganization = await service.UpdateOrganization(dto);
                    if (updatedOrganization == null)
                    {
                        logger.LogWarning("Organization with Id {OrganizationId} not found for update.", dto.OrganizationId);
                        return Results.NotFound(
                           ResponseHelper<string>.Error(
                               message: "Organization Not Found",
                               statusCode: StatusCodeEnum.NOT_FOUND
                           ).ToDictionary()
                       );
                    }

                    logger.LogInformation("Successfully updated Organization with Id {OrganizationId}.", dto.OrganizationId);
                    return Results.Ok(
                        ResponseHelper<OrganizationUpdateResponseDto>.Success(
                            message: "Organization Updated Successfully",
                            data: updatedOrganization
                        ).ToDictionary()
                    );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while updating the Organization with Id {OrganizationId}.", dto.OrganizationId);
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Updating the Organization.",
                            exception: ex,
                            isWarning: false,
                            statusCode: StatusCodeEnum.INTERNAL_SERVER_ERROR
                        ).ToDictionary()
                    );
                }
                finally
                {
                    Log.CloseAndFlush();
                }
            }).WithTags("Organization")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Updates existing Organization details", description: "This endpoint allows you to update Organization details with the provided Id."
            ));

            /// <summary> 
            /// Deletes a Organization. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint allows you to delete a Organization based on the provided Organization Id.</remarks>
            app.MapDelete("/DeleteOrganization", async (IOrganizationService service, [FromBody] OrganizationDeleteRequestDto dto, IOrganizationLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(dto);
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Deleting Organization with Id {OrganizationId}.", dto.OrganizationId);

                var validator = new OrganizationDeleteRequestValidator();
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for deleting Organization with Id {OrganizationId}: {Errors}", dto.OrganizationId, string.Join(", ", errorMessages));
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
                    var result = await service.DeleteOrganization(dto);
                    if (result == null)
                    {
                        logger.LogWarning("Organization with ID {OrganizationId} not found for deletion.", dto.OrganizationId);
                        return Results.NotFound(
                           ResponseHelper<string>.Error(
                               message: "Organization Not Found",
                               statusCode: StatusCodeEnum.NOT_FOUND
                           ).ToDictionary()
                       );
                    }

                    logger.LogInformation("Successfully deleted Organization with Id {OrganizationId}.", dto.OrganizationId);
                    return Results.Ok(
                       ResponseHelper<OrganizationDeleteResponseDto>.Success(
                           message: "Organization Deleted Successfully",
                           data: result
                       ).ToDictionary()
                   );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while deleting the Organization with Id {OrganizationId}.", dto.OrganizationId);
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Deleting the Organization.",
                            exception: ex,
                            isWarning: false,
                            statusCode: StatusCodeEnum.INTERNAL_SERVER_ERROR
                        ).ToDictionary()
                    );
                }
                finally
                {
                    Log.CloseAndFlush();
                }
            }).WithTags("Organization")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Deletes a Organization. ", description: "This endpoint allows you to delete a Organization based on the provided Organization Id."
            ));
        }
    }
}



