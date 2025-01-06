using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.Tenant.Subdomain.SubdomainRequestDto;
using HRMS.Dtos.Tenant.Subdomain.SubdomainResponseDto;
using HRMS.Utility.Helpers.Enums;
using HRMS.Utility.Helpers.Handlers;
using HRMS.Utility.Validators.Tenant.Subdomain;
using Microsoft.AspNetCore.Mvc;
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
            app.MapGet("/GetSubdomains", async (ISubdomainService service) =>
            {
                var subdomains = await service.GetSubdomains();
                if (subdomains != null && subdomains.Any())
                {
                    var response = ResponseHelper<List<SubdomainReadResponseDto>>.Success("Subdomains Retrieved Successfully", subdomains.ToList());
                    return Results.Ok(response.ToDictionary());
                }

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
            app.MapGet("/GetSubdomainById/{id}", async (ISubdomainService service, int id) =>
            {
                var validator = new SubdomainReadRequestValidator();
                var subdomainRequestDto = new SubdomainReadRequestDto { SubdomainId = id };

                var validationResult = validator.Validate(subdomainRequestDto);
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
                    var subdomain = await service.GetSubdomainById(id);
                    if (subdomain == null)
                    {
                        return Results.NotFound(
                            ResponseHelper<string>.Error(
                                message: "Subdomain Not Found",
                                statusCode: StatusCodeEnum.NOT_FOUND
                            ).ToDictionary()
                        );
                    }

                    return Results.Ok(
                        ResponseHelper<SubdomainReadResponseDto>.Success(
                            message: "Subdomain Retrieved Successfully",
                            data: subdomain
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
            app.MapPost("/CreateSubdomain", async (SubdomainCreateRequestDto dto, ISubdomainService _subdomainservice) =>
            {
                var validator = new SubdomainCreateRequestValidator();
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
                    var newUser = await _subdomainservice.CreateSubdomain(dto);
                    return Results.Ok(
                        ResponseHelper<SubdomainCreateResponseDto>.Success(
                            message: "Subdomain Created Successfully",
                            data: newUser
                        ).ToDictionary()
                    );
                }
                catch (Exception ex)
                {
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Creating the Subdomain.",
                            exception: ex,
                            isWarning: false,
                            statusCode: StatusCodeEnum.INTERNAL_SERVER_ERROR
                        ).ToDictionary()
                    );
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
            app.MapPut("/UpdateSubdomain", async (SubdomainUpdateRequestDto dto, ISubdomainService _subdomainservice) =>
            {
                var validator = new SubdomainUpdateRequestValidator();
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
                    var updatedSubdomain = await _subdomainservice.UpdateSubdomain(dto);
                    return Results.Ok(
                        ResponseHelper<SubdomainUpdateResponseDto>.Success(
                            message: "Subdomain Updated Successfully",
                            data: updatedSubdomain
                        ).ToDictionary()
                    );
                }
                catch (Exception ex)
                {
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Updating the Subdomain.",
                            exception: ex,
                            isWarning: false,
                            statusCode: StatusCodeEnum.INTERNAL_SERVER_ERROR
                        ).ToDictionary()
                    );
                }
            }).WithTags("Subdomain")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Updates existing Subdomain details", description: "This endpoint allows you to update Subdomain details with the provided Id."
            ));

            /// <summary> 
            /// Deletes a Subdomain. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint allows you to delete a Subdomain based on the provided Subdomain Id.</remarks>
            app.MapDelete("/DeleteSubdomain", async (ISubdomainService service, [FromBody] SubdomainDeleteRequestDto dto) =>
            {
                var validator = new SubdomainDeleteRequestValidator();
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
                    var result = await service.DeleteSubdomain(dto);
                    if (result == null)
                    {
                        return Results.NotFound(
                           ResponseHelper<string>.Error(
                               message: "Subdomain Not Found",
                               statusCode: StatusCodeEnum.NOT_FOUND
                           ).ToDictionary()
                       );
                    }

                    return Results.Ok(
                       ResponseHelper<SubdomainDeleteResponseDto>.Success(
                           message: "Subdomain Deleted Successfully",
                           data: result
                       ).ToDictionary()
                   );
                }
                catch (Exception ex)
                {
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Deleting the Subdomain.",
                            exception: ex,
                            isWarning: false,
                            statusCode: StatusCodeEnum.INTERNAL_SERVER_ERROR
                        ).ToDictionary()
                    );
                }
            }).WithTags("Subdomain")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Deletes a Subdomain. ", description: "This endpoint allows you to delete a Subdomain based on the provided Subdomain Id."
            ));
        }
    }
}
