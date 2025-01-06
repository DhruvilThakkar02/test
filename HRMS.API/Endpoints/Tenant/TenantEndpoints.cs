using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.Tenant.Tenant.TenantRequestDtos;
using HRMS.Dtos.Tenant.Tenant.TenantResponseDtos;
using HRMS.Utility.Validators.Tenant.Tenant;
using Microsoft.AspNetCore.Mvc;
using HRMS.Utility.Helpers.Handlers;
using Swashbuckle.AspNetCore.Annotations;
using HRMS.Utility.Helpers.Enums;

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
            app.MapGet("/GetTenants", async (ITenantService service) =>
            {
                var tenant = await service.GetTenants();
                if (tenant != null && tenant.Any())
                {
                    var response = ResponseHelper<List<TenantReadResponseDtos>>.Success("Tenants Retrieved Successfully", tenant.ToList());
                    return Results.Ok(response.ToDictionary());
                }
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
            app.MapGet("/GetTenantById/{id}", async (ITenantService service, int id) =>
            {
                var validator = new TenantReadRequestValidator();
                var tenantRequestDto = new TenantReadRequestDtos { TenantId = id };

                var validationResult = validator.Validate(tenantRequestDto);
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
                    var tenant = await service.GetTenantById(id);
                    if (tenant == null)
                    {
                        return Results.NotFound(
                            ResponseHelper<string>.Error(
                                message: "Tenant Not Found",
                                statusCode: StatusCodeEnum.NOT_FOUND
                            ).ToDictionary()
                        );
                    }

                    return Results.Ok(
                        ResponseHelper<TenantReadResponseDtos>.Success(
                            message: "Tenant Retrieved Successfully",
                            data: tenant
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
            app.MapPost("/CreateTenant", async (TenantCreateRequestDtos dto, ITenantService _service) =>
            {
                var validator = new TenantCreateRequestValidator();
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
                    var newUser = await _service.CreateTenant(dto);
                    return Results.Ok(
                        ResponseHelper<TenantCreateResponseDtos>.Success(
                            message: "Tenant Created Successfully",
                            data: newUser
                        ).ToDictionary()
                    );
                }
                catch (Exception ex)
                {
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Creating the Tenant.",
                            exception: ex,
                            isWarning: false,
                            statusCode: StatusCodeEnum.INTERNAL_SERVER_ERROR
                        ).ToDictionary()
                    );
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
            app.MapPut("/UpdateTenant", async (ITenantService _service, [FromBody] TenantUpdateRequestDtos dto) =>
            {
                var validator = new TenantUpdateRequestValidator();
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
                    var updatedTenant = await _service.UpdateTenant(dto);
                    if (updatedTenant == null)
                    {
                        return Results.NotFound(
                           ResponseHelper<string>.Error(
                               message: "Tenant Not Found",
                               statusCode: StatusCodeEnum.NOT_FOUND
                           ).ToDictionary()
                       );
                    }

                    return Results.Ok(
                        ResponseHelper<TenantUpdateResponseDtos>.Success(
                            message: "Tenant Updated Successfully",
                            data: updatedTenant
                        ).ToDictionary()
                    );
                }
                catch (Exception ex)
                {
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Updating the Tenant.",
                            exception: ex,
                            isWarning: false,
                            statusCode: StatusCodeEnum.INTERNAL_SERVER_ERROR
                        ).ToDictionary()
                    );
                }
            }).WithTags("Tenant")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Updates existing Tenant details", description: "This endpoint allows you to update Tenant details with the provided Id."
            ));

            /// <summary> 
            /// Deletes a Tenant. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint allows you to delete a Tenant based on the provided Tenant Id.</remarks>
            app.MapDelete("/DeleteTenant", async (ITenantService service, [FromBody] TenantDeleteRequestDtos dto) =>
            {
                var validator = new TenantDeleteRequestValidator();
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
                    var result = await service.DeleteTenant(dto);
                    if (result == null)
                    {
                        return Results.NotFound(
                           ResponseHelper<string>.Error(
                               message: "Tenant Not Found",
                               statusCode: StatusCodeEnum.NOT_FOUND
                           ).ToDictionary()
                       );
                    }

                    return Results.Ok(
                       ResponseHelper<TenantDeleteResponseDtos>.Success(
                           message: "Tenant Deleted Successfully",
                           data: result
                       ).ToDictionary()
                   );
                }
                catch (Exception ex)
                {
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Deleting the Tenant.",
                            exception: ex,
                            isWarning: false,
                            statusCode: StatusCodeEnum.INTERNAL_SERVER_ERROR
                        ).ToDictionary()
                    );
                }
            }).WithTags("Tenant")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Deletes a Tenant. ", description: "This endpoint allows you to delete a Tenant based on the provided Tenant Id."
            ));
        }
    }
}
