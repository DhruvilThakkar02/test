using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.Tenant.Company.CompanyRequestDtos;
using HRMS.Dtos.Tenant.Company.CompanyResponseDtos;
using HRMS.Dtos.User.User.UserRequestDtos;
using HRMS.Utility.Helpers.Enums;
using HRMS.Utility.Helpers.Handlers;
using HRMS.Utility.Validators.Tenant.Company;
using Microsoft.AspNetCore.Mvc;
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
            app.MapGet("/company/getall", async (ICompanyService service) =>
            {
                var Companys = await service.GetCompanys();
                if (Companys != null && Companys.Any())
                {
                    var response = ResponseHelper<List<CompanyReadResponseDto>>.Success("Companys Retrieved Successfully", Companys.ToList());
                    return Results.Ok(response.ToDictionary());
                }

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
            app.MapGet("/company/{id}", async (ICompanyService service, int id) =>
            {
                var validator = new CompanyReadRequestValidator();
                var CompanyRequestDto = new CompanyReadRequestDto { CompanyId = id };

                var validationResult = validator.Validate(CompanyRequestDto);
                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
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
                        return Results.NotFound(
                            ResponseHelper<string>.Error(
                                message: "Company Not Found",
                                statusCode: StatusCode.NOT_FOUND
                            ).ToDictionary()
                        );
                    }

                    return Results.Ok(
                        ResponseHelper<CompanyReadResponseDto>.Success(
                            message: "Company Retrieved Successfully",
                            data: Company
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
                            statusCode: StatusCode.INTERNAL_SERVER_ERROR
                        ).ToDictionary()
                    );
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
            app.MapPost("/company/create", async (CompanyCreateRequestDto dto, ICompanyService _companyService) =>
            {
                var validator = new CompanyCreateRequestValidator();
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
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
                    return Results.Ok(
                        ResponseHelper<CompanyCreateResponseDto>.Success(
                            message: "Company Created Successfully",
                            data: newCompany
                        ).ToDictionary()
                    );
                }
                catch (Exception ex)
                {
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Creating the Company.",
                            exception: ex,
                            isWarning: false,
                            statusCode: StatusCode.INTERNAL_SERVER_ERROR
                        ).ToDictionary()
                    );
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
            app.MapPut("/UpdateCompany", async (ICompanyService service, [FromBody] CompanyUpdateRequestDto dto) =>
            {
                var validator = new CompanyUpdateRequestValidator();
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

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
                        return Results.NotFound(
                           ResponseHelper<string>.Error(
                               message: "Company Not Found",
                               statusCode: StatusCode.NOT_FOUND
                           ).ToDictionary()
                       );
                    }

                    return Results.Ok(
                        ResponseHelper<CompanyUpdateResponseDto>.Success(
                            message: "Company Updated Successfully",
                            data: updatedCompany
                        ).ToDictionary()
                    );
                }
                catch (Exception ex)
                {
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Updating the Company.",
                            exception: ex,
                            isWarning: false,
                            statusCode: StatusCode.INTERNAL_SERVER_ERROR
                        ).ToDictionary()
                    );
                }
            }).WithTags("Company")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Updates existing Company details", description: "This endpoint allows you to update Company details with the provided Id."
            ));

            /// <summary> 
            /// Deletes a Company. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint allows you to delete a Company based on the provided Company Id.</remarks>
            app.MapDelete("/Company/delete", async (ICompanyService service, [FromBody] CompanyDeleteRequestDto dto) =>
            {
                var validator = new CompanyDeleteRequestValidator();
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

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
                        return Results.NotFound(
                           ResponseHelper<string>.Error(
                               message: "Company Not Found",
                               statusCode: StatusCode.NOT_FOUND
                           ).ToDictionary()
                       );
                    }

                    return Results.Ok(
                       ResponseHelper<CompanyDeleteResponseDto>.Success(
                           message: "Company Deleted Successfully"
                       ).ToDictionary()
                   );
                }
                catch (Exception ex)
                {
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Deleting the Company.",
                            exception: ex,
                            isWarning: false,
                            statusCode: StatusCode.INTERNAL_SERVER_ERROR
                        ).ToDictionary()
                    );
                }
            }).WithTags("Company")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Deletes a Company. ", description: "This endpoint allows you to delete a Company based on the provided Company Id."
            ));
        }
    }
}
