using FluentValidation;
using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.Address.Address.AddressRequestDtos;
using HRMS.Dtos.Address.Address.AddressResponseDtos;
using HRMS.Dtos.User.User.UserRequestDtos;
using HRMS.Utility.Helpers.Enums;
using HRMS.Utility.Helpers.Handlers;
using HRMS.Utility.Helpers.LogHelpers.Interface;
using HRMS.Utility.Validators.Address.Address;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;

namespace HRMS.API.Endpoints.Address
{
    public  static class AddressEndpoints
    {
        public static void MapAddressEndpoints(this IEndpointRouteBuilder app)
        {
            /// <summary> 
            /// Retrieves a List of Users. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint returns a List of Users. If no Users are found, a 404 status code is returned. 
            /// </remarks> 
            /// <returns>A List of Users or a 404 status code if no Users are found.</returns>
            app.MapGet("/address/getall", async (IAddressService service,IAddressLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(new
                {
                    service
                });
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Fetching all Address.");
                var alladdress = await service.GetAddresses();
                if (alladdress != null && alladdress.Any())
                {
                    var response = ResponseHelper<List<AddressReadResponseDto>>.Success("Address Retrieved Successfully", alladdress.ToList());
                    logger.LogInformation("Successfully retrieved {Count} Organizations.", alladdress.Count);
                    return Results.Ok(response.ToDictionary());
                }
                logger.LogWarning("No Addresses found.");
                var errorResponse = ResponseHelper<List<AddressReadResponseDto>>.Error("No Addresses Found");
                return Results.NotFound(errorResponse.ToDictionary());
            }).WithTags("Address")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Retrieves a List of Address", description: "This endpoint returns a List of Address. If no Address are found, a 404 status code is returned."
            ));

            /// <summary> 
            /// Retrieve Address by Id. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint return User by Id. If no User are found, a 404 status code is returned. 
            /// </remarks> 
            /// <returns>A User or a 404 status code if no User are found.</returns>
            app.MapGet("/address/{id}", async (IAddressService service, int id, IAddressLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(new
                {
                    id
                });
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Fetching Address with Id {AddressId}.", id);


                var validator = new AddressReadRequestValidator();
                var AddressRequestDto = new AddressReadRequestDto { AddressId = id };

                var validationResult = validator.Validate(AddressRequestDto);
                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                  
                    logger.LogWarning("Validation failed for Address with Id {OrganizationId}: {Errors}", id, string.Join(", ", errorMessages));
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
                    var address = await service.GetAddressById(id);
                    if (address == null)
                    {
                        logger.LogWarning("Address with Id {AddressId} not found.", id);
                        return Results.NotFound(
                            ResponseHelper<string>.Error(
                                message: "Address Not Found",
                                statusCode: StatusCode.NOT_FOUND
                            ).ToDictionary()
                        );
                    }
                    logger.LogInformation("Successfully retrieved Address with Id {AddressId}.", id);
                    return Results.Ok(
                        ResponseHelper<AddressReadResponseDto>.Success(
                            message: "Address Retrieved Successfully",
                            data: address
                        ).ToDictionary()
                    );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while retrieving the Address with Id {AddressId}.", id);
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
            }).WithTags("Address")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Retrieve Address by Id", description: "This endpoint return Address by Id. If no Address are found, a 404 status code is returned."
            ));

            /// <summary> 
            /// Creates a new User. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint allows you to create a new User with the provided details. 
            /// </remarks> 
            ///<returns> A success or error response based on the operation result.</returns >
            app.MapPost("/address/create", async (AddressCreateRequestDto dto, IAddressService _addressService, IAddressLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(dto);
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Creating new Address with data: {AddressData}", dto);
                var validator = new AddressCreateRequestValidator();
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for creating Address: {Errors}", string.Join(", ", errorMessages));
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
                    var newAddress = await _addressService.CreateAddress(dto);
                    logger.LogInformation("Successfully created Address with Id {AddressId}.", newAddress.AddressId);
                    return Results.Ok(
                        ResponseHelper<AddressCreateResponseDto>.Success(
                            message: "Address Created Successfully",
                            data: newAddress
                        ).ToDictionary()
                    );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while creating the Address.");
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Creating the Address.",
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
            }).WithTags("Address")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Creates a new Address.", description: "This endpoint allows you to create a new Address with the provided details."
            ));

            /// <summary> 
            /// Updates existing User details. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint allows you to update User details with the provided Id. 
            /// </remarks> 
            ///<returns> A success or error response based on the operation result.</returns >
            app.MapPut("/address/update", async (IAddressService service, [FromBody] AddressUpdateRequestDto dto,IAddressLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(dto);
                var validator = new AddressUpdateRequestValidator();
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Updating Address with ID {AddressId}.", dto.AddressId);
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for updating Address with Id {AddressId}: {Errors}", dto.AddressId, string.Join(", ", errorMessages));
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
                    var updatedAddress = await service.UpdateAddress(dto);
                    if (updatedAddress == null)
                    {
                        logger.LogWarning("Address with Id {AddressId} not found for update.", dto.AddressId);
                        return Results.NotFound(
                           ResponseHelper<string>.Error(
                               message: "Address Not Found",
                               statusCode: StatusCode.NOT_FOUND
                           ).ToDictionary()
                       );
                    }
                    logger.LogInformation("Successfully updated Address with Id {AddressId}.", dto.AddressId);
                    return Results.Ok(
                        ResponseHelper<AddressUpdateResponseDto>.Success(
                            message: "Address Updated Successfully",
                            data: updatedAddress
                        ).ToDictionary()
                    );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while updating the Address with Id {AddressId}.", dto.AddressId);
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Updating the Address.",
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
            }).WithTags("Address")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Updates existing Address details", description: "This endpoint allows you to update Address details with the provided Id."
            ));

            /// <summary> 
            /// Deletes a User. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint allows you to delete a User based on the provided User Id.</remarks>
            app.MapDelete("/address/delete", async (IAddressService service, [FromBody] AddressDeleteRequestDto dto, IAddressLogger logger) =>
            {
                var requestJson = JsonConvert.SerializeObject(dto);
                logger.LogInformation("Received request: {RequestJson}", requestJson);

                logger.LogInformation("Deleting Address with Id {AddressId}.", dto.AddressId);

                var validator = new AddressDeleteRequestValidator();
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Validation failed for deleting Address with Id {AddressId}: {Errors}", dto.AddressId, string.Join(", ", errorMessages));
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
                    var result = await service.DeleteAddress(dto);
                    if (result == null)
                    {
                        logger.LogWarning("Address with ID {AddressId} not found for deletion.", dto.AddressId);
                        return Results.NotFound(
                           ResponseHelper<string>.Error(
                               message: "Address Not Found",
                               statusCode: StatusCode.NOT_FOUND
                           ).ToDictionary()
                       );
                    }
                    logger.LogInformation("Successfully deleted Address with Id {AddressId}.", dto.AddressId);
                    return Results.Ok(
                       ResponseHelper<AddressDeleteResponseDto>.Success(
                           message: "Address Deleted Successfully",
                           data: result
                       ).ToDictionary()
                   );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unexpected error occurred while deleting the Address with Id {AddressId}.", dto.AddressId);
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Deleting the Address.",
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
            }).WithTags("Address")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Deletes a Address. ", description: "This endpoint allows you to delete a Address based on the provided  AddressId."
            ));
        }
    }
}
