using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.Tenant.TenantRegistration.TenantRegistrationRequestDtos;
using HRMS.Dtos.Tenant.TenantRegistration.TenantRegistrationResponseDtos;
using HRMS.Utility.Helpers.Enums;
using HRMS.Utility.Helpers.Handlers;
using HRMS.Utility.Validators.Tenant.TenantRegistration;
using Swashbuckle.AspNetCore.Annotations;

namespace HRMS.API.Endpoints.Tenant
{
    public static class TenantRegistrationEndpoints
    {
        public static void MapTenantRegistrationEndpoints(this IEndpointRouteBuilder app)
        {
            /// <summary> 
            /// Creates a new Tenant Registration. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint allows you to create a new Tenant Registration with the provided details. 
            /// </remarks> 
            ///<returns> A success or error response based on the operation result.</returns >
            app.MapPost("/CreateTenantRegistration", async (TenantRegistrationCreateRequestDto dto, ITenantRegistrationService _tenantRegistrationService) =>
            {
                var validator = new TenantRegistrationCreateRequestValidator();
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
                    var newUser = await _tenantRegistrationService.CreateTenantRegistration(dto);
                    return Results.Ok(
                        ResponseHelper<TenantRegistrationCreateResponseDto>.Success(
                            message: "Tenant Registration Created Successfully",
                            data: newUser
                        ).ToDictionary()
                    );
                }
                catch (Exception ex)
                {
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Creating the Tenant Registration.",
                            exception: ex,
                            isWarning: false,
                            statusCode: StatusCodeEnum.INTERNAL_SERVER_ERROR
                        ).ToDictionary()
                    );
                }
            }).WithTags("Tenant Registration")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Creates a new Tenant Registration.", description: "This endpoint allows you to create a new Tenant Registration with the provided details."
            ));
        }
    }
}
