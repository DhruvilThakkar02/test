using FluentValidation;
using FluentValidation.Results;
using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.Address.AddressType.AddressTypeRequestDtos;
using HRMS.Dtos.Address.AddressType.AddressTypeResponseDtos;

using HRMS.Utility.Helpers.Enums;
using HRMS.Utility.Helpers.Handlers;
using HRMS.Utility.Helpers.LogHelpers.Interface;
using HRMS.Utility.Validators.Address.AddressType;

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;

namespace HRMS.API.Endpoints.Address
{
    public  static class AddressTypeEndpoints
    {
        public static void MapAddressTypeEndpoints(this IEndpointRouteBuilder app)
        {
            /// <summary> 
            /// Retrieves a List of AddressTypes. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint returns a List of AddressTypes If no AddressTypes are found, a 404 status code is returned. 
            /// </remarks> 
            /// <returns>A List of AddressTypes or a 404 status code if no AddressTypes are found.</returns>
            app.MapGet("/addresstype/getall", async (IAddressTypeService service, IAddressTypeLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(new
                {
                    service
                });
              
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Fetching all AddressTypes.");
                var addresstypes = await service.GetAddressTypes();
                if (addresstypes != null && addresstypes.Any())
                {
                    var response = ResponseHelper<List<AddressTypeReadResponseDto>>.Success("AddressTypes Retrieved Successfully", addresstypes.ToList());
                    return Results.Ok(response.ToDictionary());
                }
                logger.LogWarning("No AddressTypes found.");
                var errorResponse = ResponseHelper<List<AddressTypeReadResponseDto>>.Error("No AddressTypes Found");
                return Results.NotFound(errorResponse.ToDictionary());
            }).WithTags("AddressType")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Retrieves a List of AddressTypes", description: "This endpoint returns a List of AddressTypes. If no AddressTypes are found, a 404 status code is returned."
            ));

            /// <summary> 
            /// Retrieve AddressType by Id. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint return AddressType by Id. If no AddressType are found, a 404 status code is returned. 
            /// </remarks> 
            /// <returns>A AddressType or a 404 status code if no AddressType are found.</returns>
            app.MapGet("/addresstype/{id}", async (IAddressTypeService service, int id,IAddressTypeLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(new
                {
                    id
                });
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Fetching Organization with Id {OrganizationId}.", id);
                var validator = new AddressTypeReadRequestValidator();
                var addresstypeRequestDto = new AddressTypeReadRequestDto { AddressTypeId = id };

                var validationResult = validator.Validate(addresstypeRequestDto);
                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for AddressType with Id {AddressTypeId}: {Errors}", id, string.Join(", ", errorMessages));
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
                    var addresstype = await service.GetAddressTypeById(id);
                    if (addresstype == null)
                    {
                        return Results.NotFound(
                            ResponseHelper<string>.Error(
                                message: "AddressType Not Found",
                                statusCode: StatusCode.NOT_FOUND
                            ).ToDictionary()
                        );
                    }
                    logger.LogInformation("Successfully retrieved AddressType with Id {OrganizationId}.", id);
                    return Results.Ok(
                        ResponseHelper<AddressTypeReadResponseDto>.Success(
                            message: "AddressType Retrieved Successfully",
                            data: addresstype
                        ).ToDictionary()
                    );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while retrieving the AddressType with Id {AddressTypeId}.", id);
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
            }).WithTags("AddressType")
           .WithMetadata(new SwaggerOperationAttribute(summary: "Retrieve AddressType by Id", description: "This endpoint return AddressType by Id. If no AddressType are found, a 404 status code is returned."
           ));

            /// <summary> 
            /// Creates a new AddressType. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint allows you to create a new AddressType with the provided details. 
            /// </remarks> 
            ///<returns> A success or error response based on the operation result.</returns >
            app.MapPost("/addresstype/create", async (AddressTypeCreateRequestDto dto, IAddressTypeService _addresstypeService,IAddressTypeLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(dto);
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Creating new AddressType with data: {AddressTypeData}", dto);
                var validator = new AddressTypeCreateRequestValidator();
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for creating AddressType: {Errors}", string.Join(", ", errorMessages));
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
                    var newAddresstype = await _addresstypeService.CreateAddressType(dto);
                    logger.LogInformation("Successfully created AddressType with Id {AddressTypeId}.", newAddresstype.AddressTypeId);
                    return Results.Ok(
                        ResponseHelper<AddressTypeCreateResponseDto>.Success(
                            message: "AddressType Created Successfully",
                            data: newAddresstype
                        ).ToDictionary()
                    );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while creating the AddressType.");
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Creating the AddressType.",
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
            }).WithTags("AddressType")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Creates a new AddressType.", description: "This endpoint allows you to create a new AddressType with the provided details."
            ));

            /// <summary> 
            /// Updates existing AddressType details. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint allows you to update AddressType details with the provided Id. 
            /// </remarks> 
            ///<returns> A success or error response based on the operation result.</returns >
            app.MapPut("/addresstype/update", async (IAddressTypeService service, [FromBody] AddressTypeUpdateRequestDto dto,IAddressTypeLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(dto);
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Updating AddressType with ID {AddressTypeId}.", dto.AddressTypeId);
                var validator = new AddressTypeUpdateRequestValidator();
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for updating AddressType with Id {AddressTypeId}: {Errors}", dto.AddressTypeId, string.Join(", ", errorMessages));
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
                    var updatedAddresstype = await service.UpdateAddressType(dto);
                    if (updatedAddresstype == null)
                    {
                        logger.LogWarning("AddressType with Id {OrganizationId} not found for update.", dto.AddressTypeId);
                        return Results.NotFound(
                           ResponseHelper<string>.Error(
                               message: "AddressType Not Found",
                               statusCode: StatusCode.NOT_FOUND
                           ).ToDictionary()
                       );
                    }
                    logger.LogInformation("Successfully updated AddressType with Id {AddressTypeId}.", dto.AddressTypeId);
                    return Results.Ok(
                        ResponseHelper<AddressTypeUpdateResponseDto>.Success(
                            message: "AddressType Updated Successfully",
                            data: updatedAddresstype
                        ).ToDictionary()
                    );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while updating the AddressType with Id {AddressTypeId}.", dto.AddressTypeId);
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Updating the AddressType.",
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
            }).WithTags("AddressType")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Updates existing AddressType details", description: "This endpoint allows you to update AddressType details with the provided Id."
            ));

            /// <summary> 
            /// Deletes a AddressType. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint allows you to delete a AddressType based on the provided AddressType Id.</remarks>
            app.MapDelete("/addresstype/delete", async (IAddressTypeService service, [FromBody] AddressTypeDeleteRequestDto dto,IAddressTypeLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(dto);
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Deleting AddressType with Id {AddressTypeId}.", dto.AddressTypeId);
                var validator = new AddressTypeDeleteRequestValidator();
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for deleting AddressType with Id {AddressTypeId}: {Errors}", dto.AddressTypeId, string.Join(", ", errorMessages));
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
                    var result = await service.DeleteAddressType(dto);
                    if (result == null)
                    {
                        logger.LogWarning("AddressType with ID {AddressTypeId} not found for deletion.", dto.AddressTypeId);
                        return Results.NotFound(
                           ResponseHelper<string>.Error(
                               message: "AddressType Not Found",
                               statusCode: StatusCode.NOT_FOUND
                           ).ToDictionary()
                       );
                    }
                    logger.LogInformation("Successfully deleted Organization with Id {AddressType}.", dto.AddressTypeId);
                    return Results.Ok(
                       ResponseHelper<AddressTypeDeleteResponseDto>.Success(
                           message: "AddressType Deleted Successfully"
                       ).ToDictionary()
                   );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while deleting the AddressType with Id {AddressTypeId}.", dto.AddressTypeId);
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Deleting the AddressType.",
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
            }).WithTags("AddressType")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Deletes a AddressType. ", description: "This endpoint allows you to delete a AddressType based on the provided AddressType Id."
            ));
        }
    }
}
