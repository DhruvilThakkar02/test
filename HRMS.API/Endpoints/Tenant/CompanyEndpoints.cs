using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.Tenant.Company.CompanyRequestDtos;
using HRMS.Dtos.Tenant.Company.CompanyResponseDtos;
using HRMS.Dtos.User.User.UserRequestDtos;
using HRMS.Utility.Helpers.Enums;
using HRMS.Utility.Helpers.Handlers;
using HRMS.Utility.Helpers.LogHelpers.Interface;
using HRMS.Utility.Validators.Tenant.Company;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;

namespace HRMS.API.Endpoints.Tenant
{
    public static class CompanyEndpoints
    {
        public static void MapCompanyEndpoints(this IEndpointRouteBuilder app)
        {
            /// <summary> 
            /// Retrieves a List of Companys. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint returns a List of Companys. If no Companys are found, a 404 status code is returned. 
            /// </remarks> 
            /// <returns>A List of Companys or a 404 status code if no Companys are found.</returns>
            app.MapGet("/company/getall", async (ICompanyService service, ICompanyLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(new { service });
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Fetching all CompanyBranches.");
                var Companys = await service.GetCompanys();
                if (Companys != null && Companys.Any())
                {
                    var response = ResponseHelper<List<CompanyReadResponseDto>>.Success("Companys Retrieved Successfully", Companys.ToList());
                    logger.LogInformation("Successfully retrieved {Count} Companys.", Companys.Count());
                    return Results.Ok(response.ToDictionary());
                }

                logger.LogWarning("No Companys found.");
                var errorResponse = ResponseHelper<List<CompanyReadResponseDto>>.Error("No Companys Found");
                return Results.NotFound(errorResponse.ToDictionary());
            }).WithTags("Company")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Retrieves a List of Companys", description: "This endpoint returns a List of Companys. If no Companys are found, a 404 status code is returned."
            ));

            /// <summary> 
            /// Retrieve Company by Id. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint return Company by Id. If no Company are found, a 404 status code is returned. 
            /// </remarks> 
            /// <returns>A Company or a 404 status code if no Company are found.</returns>
            app.MapGet("/company/{id}", async (ICompanyService service, int id, ICompanyLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(new { id });
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Fetching Company with Id {CompanyId}.", id);

                var validator = new CompanyReadRequestValidator();
                var CompanyRequestDto = new CompanyReadRequestDto { CompanyId = id };

                var validationResult = validator.Validate(CompanyRequestDto);
                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for Company with Id {CompanyId}: {Errors}", id, string.Join(", ", errorMessages));
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
                    var Company = await service.GetCompanyById(id);
                    if (Company == null)
                    {
                        logger.LogWarning("Company with Id {CompanyId} not found.", id);
                        return Results.NotFound(
                            ResponseHelper<string>.Error(
                                message: "Company Not Found",
                                statusCode: StatusCode.NOT_FOUND
                            ).ToDictionary()
                        );
                    }

                    logger.LogInformation("Successfully retrieved Company with Id {CompanyId}.", id);
                    return Results.Ok(
                        ResponseHelper<CompanyReadResponseDto>.Success(
                            message: "Company Retrieved Successfully",
                            data: Company
                        ).ToDictionary()
                    );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while retrieving the Company with Id {CompanyId}.", id);
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
            }).WithTags("Company")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Retrieve Company by Id", description: "This endpoint return Company by Id. If no Company are found, a 404 status code is returned."
            ));


            /// <summary> 
            /// Creates a new Company. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint allows you to create a new Company with the provided details. 
            /// </remarks> 
            ///<returns> A success or error response based on the operation result.</returns >
            app.MapPost("/company/create", async (CompanyCreateRequestDto dto, ICompanyService _companyService, ICompanyLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(dto);
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Creating new Company with data: {CompanyData}", dto);

                var validator = new CompanyCreateRequestValidator();
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for creating Company: {Errors}", string.Join(", ", errorMessages));
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
                    var newCompany = await _companyService.CreateCompany(dto);
                    logger.LogInformation("Successfully created Company with Id {CompanyId}.", newCompany.CompanyId);
                    return Results.Ok(
                        ResponseHelper<CompanyCreateResponseDto>.Success(
                            message: "Company Created Successfully",
                            data: newCompany
                        ).ToDictionary()
                    );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while creating the Company.");
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Creating the Company.",
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
            }).WithTags("Company")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Creates a new Company.", description: "This endpoint allows you to create a new Company with the provided details."
            ));


            /// <summary> 
            /// Updates existing Company details. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint allows you to update Company details with the provided Id. 
            /// </remarks> 
            ///<returns> A success or error response based on the operation result.</returns >
            app.MapPut("/company/update", async (ICompanyService service, [FromBody] CompanyUpdateRequestDto dto) =>
            {
                var requestJson = JsonConvert.SerializeObject(dto);
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Updating Company with ID {CompanyId}.", dto.CompanyId);

                var validator = new CompanyUpdateRequestValidator();
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for updating Company with Id {CompanyId}: {Errors}", dto.CompanyId, string.Join(", ", errorMessages));
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
                    var updatedCompany = await service.UpdateCompany(dto);
                    if (updatedCompany == null)
                    {
                        logger.LogWarning("Company with Id {CompanyId} not found for update.", dto.CompanyId);
                        return Results.NotFound(
                           ResponseHelper<string>.Error(
                               message: "Company Not Found",
                               statusCode: StatusCode.NOT_FOUND
                           ).ToDictionary()
                       );
                    }

                    logger.LogInformation("Successfully updated Company with Id {CompanyId}.", dto.CompanyId);
                    return Results.Ok(
                        ResponseHelper<CompanyUpdateResponseDto>.Success(
                            message: "Company Updated Successfully",
                            data: updatedCompany
                        ).ToDictionary()
                    );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while updating the Company with Id {CompanyId}.", dto.CompanyId);
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Updating the Company.",
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
            }).WithTags("Company")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Updates existing Company details", description: "This endpoint allows you to update Company details with the provided Id."
            ));

            /// <summary> 
            /// Deletes a Company. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint allows you to delete a Company based on the provided Company Id.</remarks>
            app.MapDelete("/Company/delete", async (ICompanyService service, [FromBody] CompanyDeleteRequestDto dto, ICompanyLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(dto);
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Deleting Company with Id {CompanyId}.", dto.CompanyId);

                var validator = new CompanyDeleteRequestValidator();
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for deleting Company with Id {CompanyId}: {Errors}", dto.CompanyId, string.Join(", ", errorMessages));
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
                    var result = await service.DeleteCompany(dto);
                    if (result == null)
                    {
                        logger.LogWarning("Company with ID {CompanyId} not found for deletion.", dto.CompanyId);
                        return Results.NotFound(
                           ResponseHelper<string>.Error(
                               message: "Company Not Found",
                               statusCode: StatusCode.NOT_FOUND
                           ).ToDictionary()
                       );
                    }

                    logger.LogInformation("Successfully Deleted CompanyBranch with Id {CompanyBranchId}.", dto.CompanyId);
                    return Results.Ok(
                       ResponseHelper<CompanyDeleteResponseDto>.Success(
                           message: "Company Deleted Successfully"
                       ).ToDictionary()
                   );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while deleting the Company with Id {CompanyId}.", dto.CompanyId);
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Deleting the Company.",
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
            }).WithTags("Company")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Deletes a Company. ", description: "This endpoint allows you to delete a Company based on the provided Company Id."
            ));
        }
    }
}
