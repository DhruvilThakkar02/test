using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.Tenant.Subdomain.SubdomainRequestDto;
using HRMS.Dtos.Tenant.Subdomain.SubdomainResponseDto;
using HRMS.Utility.Helpers.Enums;
using HRMS.Utility.Helpers.Handlers;
using HRMS.Utility.Helpers.LogHelpers.Interface;
using HRMS.Utility.Validators.Tenant.Subdomain;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;

namespace HRMS.API.Endpoints.Tenant
{
    public static class SubdomainEndpoints
    {
        public static void MapSubdomainEndpoints(this IEndpointRouteBuilder app)
        {
            /// <summary> 
            /// Retrieves a List of Subdomains. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint returns a List of Subdomains. If no Subdomains are found, a 404 status code is returned. 
            /// </remarks> 
            /// <returns>A List of Subdomains or a 404 status code if no Subdomains are found.</returns>
            app.MapGet("/subdomain/getall", async (ISubdomainService service, ISubdomainLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(new { service });
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Fetching all Subdomains.");

                var subdomains = await service.GetSubdomains();
                if (subdomains != null && subdomains.Any())
                {
                    var response = ResponseHelper<List<SubdomainReadResponseDto>>.Success("Subdomains Retrieved Successfully", subdomains.ToList());
                    logger.LogInformation("Successfully retrieved {Count} CompanyBranches.", subdomains.Count());
                    return Results.Ok(response.ToDictionary());
                }

                logger.LogWarning("No Subdomains found.");
                var errorResponse = ResponseHelper<List<SubdomainReadResponseDto>>.Error("No Subdomains Found");
                return Results.NotFound(errorResponse.ToDictionary());
            }).WithTags("Subdomain")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Retrieves a List of Subdomains", description: "This endpoint returns a List of Subdomains. If no Subdomains are found, a 404 status code is returned."
            ));

            /// <summary> 
            /// Retrieve Subdomain by Id. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint return Subdomain by Id. If no Subdomain are found, a 404 status code is returned. 
            /// </remarks> 
            /// <returns>A Subdomain or a 404 status code if no Subdomain are found.</returns>
            app.MapGet("/subdomain/{id}", async (ISubdomainService service, int id, ISubdomainLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(new { id });
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Fetching Subdomain with Id {SubdomainId}.", id);

                var validator = new SubdomainReadRequestValidator();
                var subdomainRequestDto = new SubdomainReadRequestDto { SubdomainId = id };

                var validationResult = validator.Validate(subdomainRequestDto);
                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for Subdomain with Id {SubdomainId}: {Errors}", id, string.Join(", ", errorMessages));
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
                    var subdomain = await service.GetSubdomainById(id);
                    if (subdomain == null)
                    {
                        logger.LogWarning("Subdomain with Id {SubdomainId} not found.", id);
                        return Results.NotFound(
                            ResponseHelper<string>.Error(
                                message: "Subdomain Not Found",
                                statusCode: StatusCode.NOT_FOUND
                            ).ToDictionary()
                        );
                    }

                    logger.LogInformation("Successfully retrieved Subdomain with Id {SubdomainId}.", id);
                    return Results.Ok(
                        ResponseHelper<SubdomainReadResponseDto>.Success(
                            message: "Subdomain Retrieved Successfully",
                            data: subdomain
                        ).ToDictionary()
                    );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while retrieving the Subdomain with Id {SubdomainId}.", id);
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
            }).WithTags("Subdomain")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Retrieve Subdomain by Id", description: "This endpoint return Subdomain by Id. If no Subdomain are found, a 404 status code is returned."
            ));

            /// <summary> 
            /// Creates a new Subdomain. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint allows you to create a new Subdomain with the provided details. 
            /// </remarks> 
            ///<returns> A success or error response based on the operation result.</returns >
            app.MapPost("/subdomain/create", async (SubdomainCreateRequestDto dto, ISubdomainService _subdomainservice, ISubdomainLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(dto);
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Creating new Subdomain with data: {SubdomainData}", dto);

                var validator = new SubdomainCreateRequestValidator();
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for creating Subdomain: {Errors}", string.Join(", ", errorMessages));
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
                    var newUser = await _subdomainservice.CreateSubdomain(dto);
                    logger.LogInformation("Successfully created Subdomain with Id {CompanyBranchId}.", newUser.SubdomainId);
                    return Results.Ok(
                        ResponseHelper<SubdomainCreateResponseDto>.Success(
                            message: "Subdomain Created Successfully",
                            data: newUser
                        ).ToDictionary()
                    );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while creating the Subdomain.");
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Creating the Subdomain.",
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
            }).WithTags("Subdomain")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Creates a new Subdomain.", description: "This endpoint allows you to create a new Subdomain with the provided details."
            ));

            /// <summary> 
            /// Updates existing Subdomain details. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint allows you to update Subdomain details with the provided Id. 
            /// </remarks> 
            ///<returns> A success or error response based on the operation result.</returns >
            app.MapPut("/subdomain/update", async (SubdomainUpdateRequestDto dto, ISubdomainService _subdomainservice, ISubdomainLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(dto);
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Updating Subdomain with ID {SubdomainId}.", dto.SubdomainId);

                var validator = new SubdomainUpdateRequestValidator();
                var validationResult = validator.Validate(dto);
                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for updating Subdomain with Id {SubdomainId}: {Errors}", dto.SubdomainId, string.Join(", ", errorMessages));
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
                    var updatedSubdomain = await _subdomainservice.UpdateSubdomain(dto);
                    logger.LogInformation("Successfully updated Subdomain with Id {SubdomainId}.", dto.SubdomainId);
                    return Results.Ok(
                        ResponseHelper<SubdomainUpdateResponseDto>.Success(
                            message: "Subdomain Updated Successfully",
                            data: updatedSubdomain
                        ).ToDictionary()
                    );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while updating the Subdomain with Id {SubdomainId}.", dto.SubdomainId);
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Updating the Subdomain.",
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
            }).WithTags("Subdomain")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Updates existing Subdomain details", description: "This endpoint allows you to update Subdomain details with the provided Id."
            ));

            /// <summary> 
            /// Deletes a Subdomain. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint allows you to delete a Subdomain based on the provided Subdomain Id.</remarks>
            app.MapDelete("/subdomain/delete", async (ISubdomainService service, [FromBody] SubdomainDeleteRequestDto dto, ISubdomainLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(dto);
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Deleting Subdomain with Id {SubdomainId}.", dto.SubdomainId);

                var validator = new SubdomainDeleteRequestValidator();
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for deleting Subdomain with Id {SubdomainId}: {Errors}", dto.SubdomainId, string.Join(", ", errorMessages));
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
                    var result = await service.DeleteSubdomain(dto);
                    if (result == null)
                    {
                        logger.LogWarning("Subdomain with ID {SubdomainId} not found for deletion.", dto.SubdomainId);
                        return Results.NotFound(
                           ResponseHelper<string>.Error(
                               message: "Subdomain Not Found",
                               statusCode: StatusCode.NOT_FOUND
                           ).ToDictionary()
                       );
                    }

                    logger.LogInformation("Successfully Deleted Subdomain with Id {SubdomainId}.", dto.SubdomainId);
                    return Results.Ok(
                       ResponseHelper<SubdomainDeleteResponseDto>.Success(
                           message: "Subdomain Deleted Successfully"
                       ).ToDictionary()
                   );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while deleting the Subdomain with Id {SubdomainId}.", dto.SubdomainId);
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Deleting the Subdomain.",
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
            }).WithTags("Subdomain")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Deletes a Subdomain. ", description: "This endpoint allows you to delete a Subdomain based on the provided Subdomain Id."
            ));
        }
    }
}
