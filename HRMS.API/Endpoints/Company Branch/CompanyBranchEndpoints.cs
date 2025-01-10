using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.CompanyBranch.CompanyBranchRequestDtos;
using HRMS.Dtos.CompanyBranch.CompanyBranchResponseDtos;
using HRMS.Utility.Helpers.Enums;
using HRMS.Utility.Helpers.Handlers;
using HRMS.Utility.Validators.CompanyBranch;
using HRMS.Utility.Helpers.LogHelpers.Interface;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;


namespace HRMS.API.Endpoints.CompanyBranch
{
    public static class CompanyBranchEndpoints
    {
        public static void MapCompanyBranchEndpoints(this IEndpointRouteBuilder app)
        {
            /// <summary> 
            /// Retrieves a List of CompanyBranches. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint returns a List of CompanyBranches. If no CompanyBranches are found, a 404 status code is returned. 
            /// </remarks> 
            /// <returns>A List of CompanyBranches or a 404 status code if no CompanyBranches are found.</returns>
            app.MapGet("/companyBranch/getall", async (ICompanyBranchService service, ICompanyBranchLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(new { service });
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Fetching all CompanyBranches.");

                var companyBranches = await service.GetCompanyBranches();
                if (companyBranches != null && companyBranches.Any())
                {
                    var response = ResponseHelper<List<CompanyBranchReadResponseDto>>.Success("CompanyBranches Retrieved Successfully", companyBranches.ToList());
                    logger.LogInformation("Successfully retrieved {Count} CompanyBranches.", companyBranches.Count);
                    return Results.Ok(response.ToDictionary());
                }

                logger.LogWarning("No CompanyBranches found.");
                var errorResponse = ResponseHelper<IEnumerable<CompanyBranchReadResponseDto>>.Error("No CompanyBranches Found");
                return Results.NotFound(errorResponse.ToDictionary());
            }).WithTags("Company Branch")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Retrieves a List of CompanyBranches", description: "This endpoint returns a List of CompanyBranches. If no CompanyBranches are found, a 404 status code is returned."
            ));

            /// <summary> 
            /// Retrieve CompanyBranch by Id. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint returns a CompanyBranch by Id. If no CompanyBranch is found, a 404 status code is returned. 
            /// </remarks> 
            /// <returns>A CompanyBranch or a 404 status code if no CompanyBranch is found.</returns>
            app.MapGet("/companyBranch/{id}", async (ICompanyBranchService service, int id, ICompanyBranchLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(new { id });
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Fetching CompanyBranch with Id {CompanyBranchId}.", id);

                var validator = new CompanyBranchReadRequestValidator();
                var companyBranchRequestDto = new CompanyBranchReadRequestDto { CompanyBranchId = id };

                var validationResult = validator.Validate(companyBranchRequestDto);
                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for CompanyBranch with Id {CompanyBranchId}: {Errors}", id, string.Join(", ", errorMessages));
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
                    var companyBranch = await service.GetCompanyBranchById(id);
                    if (companyBranch == null)
                    {
                        logger.LogWarning("CompanyBranch with Id {CompanyBranchId} not found.", id);
                        return Results.NotFound(
                            ResponseHelper<string>.Error(
                                message: "CompanyBranch Not Found",
                                statusCode: StatusCode.NOT_FOUND
                            ).ToDictionary()
                        );
                    }

                    logger.LogInformation("Successfully retrieved CompanyBranch with Id {CompanyBranchId}.", id);
                    return Results.Ok(
                        ResponseHelper<CompanyBranchReadResponseDto>.Success(
                            message: "CompanyBranch Retrieved Successfully",
                            data: companyBranch
                        ).ToDictionary()
                    );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while retrieving the CompanyBranch with Id {CompanyBranchId}.", id);
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
            }).WithTags("Company Branch")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Retrieve CompanyBranch by Id", description: "This endpoint returns a CompanyBranch by Id. If no CompanyBranch is found, a 404 status code is returned."
            ));

            /// <summary> 
            /// Creates a new CompanyBranch. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint allows you to create a new CompanyBranch with the provided details. 
            /// </remarks> 
            ///<returns> A success or error response based on the operation result.</returns >
            app.MapPost("/companyBranch/create", async (CompanyBranchCreateRequestDto dto, ICompanyBranchService _companyBranchService, ICompanyBranchLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(dto);
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Creating new CompanyBranch with data: {CompanyBranchData}", dto);

                var validator = new CompanyBranchCreateRequestValidator();
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for creating CompanyBranch: {Errors}", string.Join(", ", errorMessages));
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
                    var newCompanyBranch = await _companyBranchService.CreateCompanyBranch(dto);
                    logger.LogInformation("Successfully created CompanyBranch with Id {CompanyBranchId}.", newCompanyBranch.CompanyBranchId);
                    return Results.Ok(
                        ResponseHelper<CompanyBranchCreateResponseDto>.Success(
                            message: "CompanyBranch Created Successfully",
                            data: newCompanyBranch
                        ).ToDictionary()
                    );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while creating the CompanyBranch.");
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Creating the CompanyBranch.",
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
            }).WithTags("Company Branch")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Creates a new CompanyBranch.", description: "This endpoint allows you to create a new CompanyBranch with the provided details."
            ));

            /// <summary> 
            /// Updates existing CompanyBranch details. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint allows you to update CompanyBranch details with the provided Id. 
            /// </remarks> 
            ///<returns> A success or error response based on the operation result.</returns >
            app.MapPut("/companyBranch/update", async (ICompanyBranchService service, [FromBody] CompanyBranchUpdateRequestDto dto, ICompanyBranchLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(dto);
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Updating CompanyBranch with ID {CompanyBranchId}.", dto.CompanyBranchId);

                var validator = new CompanyBranchUpdateRequestValidator();
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for updating CompanyBranch with Id {CompanyBranchId}: {Errors}", dto.CompanyBranchId, string.Join(", ", errorMessages));
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
                    var updatedCompanyBranch = await service.UpdateCompanyBranch(dto);
                    if (updatedCompanyBranch == null)
                    {
                        logger.LogWarning("CompanyBranch with Id {CompanyBranchId} not found for update.", dto.CompanyBranchId);
                        return Results.NotFound(
                           ResponseHelper<string>.Error(
                               message: "CompanyBranch Not Found",
                               statusCode: StatusCode.NOT_FOUND
                           ).ToDictionary()
                       );
                    }

                    logger.LogInformation("Successfully updated CompanyBranch with Id {CompanyBranchId}.", dto.CompanyBranchId);
                    return Results.Ok(
                        ResponseHelper<CompanyBranchUpdateResponseDto>.Success(
                            message: "CompanyBranch Updated Successfully",
                            data: updatedCompanyBranch
                        ).ToDictionary()
                    );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while updating the CompanyBranch with Id {CompanyBranchId}.", dto.CompanyBranchId);
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Updating the CompanyBranch.",
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
            }).WithTags("Company Branch")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Updates existing CompanyBranch details", description: "This endpoint allows you to update CompanyBranch details with the provided Id."
            ));

            /// <summary> 
            /// Deletes a CompanyBranch. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint allows you to delete a CompanyBranch based on the provided CompanyBranch Id.</remarks>
            app.MapDelete("/companyBranch/delete", async (ICompanyBranchService service, [FromBody] CompanyBranchDeleteRequestDto dto, ICompanyBranchLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(dto);
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Deleting CompanyBranch with Id {CompanyBranchId}.", dto.CompanyBranchId);

                var validator = new CompanyBranchDeleteRequestValidator();
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for deleting CompanyBranch with Id {CompanyBranchId}: {Errors}", dto.CompanyBranchId, string.Join(", ", errorMessages));
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
                    var result = await service.DeleteCompanyBranch(dto);
                    if (result == null)
                    {
                        logger.LogWarning("CompanyBranch with ID {CompanyBranchId} not found for deletion.", dto.CompanyBranchId);
                        return Results.NotFound(
                           ResponseHelper<string>.Error(
                               message: "CompanyBranch Not Found",
                               statusCode: StatusCode.NOT_FOUND
                           ).ToDictionary()
                       );
                    }

                    logger.LogInformation("Successfully Deleted CompanyBranch with Id {CompanyBranchId}.", dto.CompanyBranchId);
                    return Results.Ok(
                       ResponseHelper<CompanyBranchDeleteResponseDto>.Success(
                           message: "CompanyBranch Deleted Successfully",
                           data: result
                       ).ToDictionary()
                   );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while deleting the CompanyBranch with Id {CompanyBranchId}.", dto.CompanyBranchId);
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Deleting the CompanyBranch.",
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
            }).WithTags("Company  Branch")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Deletes a CompanyBranch.", description: "This endpoint allows you to delete a CompanyBranch based on the provided CompanyBranch Id."
            ));
        }
    }
}
