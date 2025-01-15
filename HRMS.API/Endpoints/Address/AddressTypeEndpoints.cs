using FluentValidation;
using FluentValidation.Results;
using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.Address.AddressType.AddressTypeRequestDtos;
using HRMS.Dtos.Address.AddressType.AddressTypeResponseDtos;

using HRMS.Utility.Helpers.Enums;
using HRMS.Utility.Helpers.Handlers;
using HRMS.Utility.Validators.Address.AddressType;
using HRMS.Utility.Validators.User.User;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HRMS.API.Endpoints.Address
{
    public  static class AddressTypeEndpoints
    {
        public static void MapAddressTypeEndpoints(this IEndpointRouteBuilder app)
        {
            /// <summary> 
            /// Retrieves a List of Users. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint returns a List of Users. If no Users are found, a 404 status code is returned. 
            /// </remarks> 
            /// <returns>A List of Users or a 404 status code if no Users are found.</returns>
            app.MapGet("/addresstype/getall", async (IAddressTypeService service) =>
            {
                var addresstypes = await service.GetAddressTypes();
                if (addresstypes != null && addresstypes.Any())
                {
                    var response = ResponseHelper<List<AddressTypeReadResponseDto>>.Success("AddressTypes Retrieved Successfully", addresstypes.ToList());
                    return Results.Ok(response.ToDictionary());
                }

                var errorResponse = ResponseHelper<List<AddressTypeReadResponseDto>>.Error("No AddressTypes Found");
                return Results.NotFound(errorResponse.ToDictionary());
            }).WithTags("AddressType")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Retrieves a List of AddressTypes", description: "This endpoint returns a List of AddressTypes. If no AddressTypes are found, a 404 status code is returned."
            ));

            /// <summary> 
            /// Retrieve User by Id. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint return User by Id. If no User are found, a 404 status code is returned. 
            /// </remarks> 
            /// <returns>A User or a 404 status code if no User are found.</returns>
            app.MapGet("/addresstype/{id}", async (IAddressTypeService service, int id) =>
            {
                var validator = new AddressTypeReadRequestValidator();
                var addresstypeRequestDto = new AddressTypeReadRequestDto { AddressTypeId = id };

                var validationResult = validator.Validate(addresstypeRequestDto);
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

                    return Results.Ok(
                        ResponseHelper<AddressTypeReadResponseDto>.Success(
                            message: "User Retrieved Successfully",
                            data: addresstype
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
            }).WithTags("AddressType")
           .WithMetadata(new SwaggerOperationAttribute(summary: "Retrieve AddressType by Id", description: "This endpoint return AddressType by Id. If no User are found, a 404 status code is returned."
           ));

            /// <summary> 
            /// Creates a new User. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint allows you to create a new User with the provided details. 
            /// </remarks> 
            ///<returns> A success or error response based on the operation result.</returns >
            app.MapPost("/addresstype/create", async (AddressTypeCreateRequestDto dto, IAddressTypeService _addresstypeService) =>
            {
                var validator = new AddressTypeCreateRequestValidator();
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
                    var newAddresstype = await _addresstypeService.CreateAddressType(dto);
                    return Results.Ok(
                        ResponseHelper<AddressTypeCreateResponseDto>.Success(
                            message: "AddressType Created Successfully",
                            data: newAddresstype
                        ).ToDictionary()
                    );
                }
                catch (Exception ex)
                {
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Creating the AddressType.",
                            exception: ex,
                            isWarning: false,
                            statusCode: StatusCode.INTERNAL_SERVER_ERROR
                        ).ToDictionary()
                    );
                }
            }).WithTags("AddressType")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Creates a new AddressType.", description: "This endpoint allows you to create a new AddressType with the provided details."
            ));

            /// <summary> 
            /// Updates existing User details. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint allows you to update User details with the provided Id. 
            /// </remarks> 
            ///<returns> A success or error response based on the operation result.</returns >
            app.MapPut("/addresstype/update", async (IAddressTypeService service, [FromBody] AddressTypeUpdateRequestDto dto) =>
            {
                var validator = new AddressTypeUpdateRequestValidator();
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
                    var updatedAddresstype = await service.UpdateAddressType(dto);
                    if (updatedAddresstype == null)
                    {
                        return Results.NotFound(
                           ResponseHelper<string>.Error(
                               message: "AddressType Not Found",
                               statusCode: StatusCode.NOT_FOUND
                           ).ToDictionary()
                       );
                    }

                    return Results.Ok(
                        ResponseHelper<AddressTypeUpdateResponseDto>.Success(
                            message: "AddressType Updated Successfully",
                            data: updatedAddresstype
                        ).ToDictionary()
                    );
                }
                catch (Exception ex)
                {
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Updating the AddressType.",
                            exception: ex,
                            isWarning: false,
                            statusCode: StatusCode.INTERNAL_SERVER_ERROR
                        ).ToDictionary()
                    );
                }
            }).WithTags("AddressType")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Updates existing AddressType details", description: "This endpoint allows you to update AddressType details with the provided Id."
            ));

            /// <summary> 
            /// Deletes a User. 
            /// </summary> 
            /// <remarks> 
            /// This endpoint allows you to delete a User based on the provided User Id.</remarks>
            app.MapDelete("/addresstype/delete", async (IAddressTypeService service, [FromBody] AddressTypeDeleteRequestDto dto) =>
            {
                var validator = new AddressTypeDeleteRequestValidator();
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
                    var result = await service.DeleteAddressType(dto);
                    if (result == null)
                    {
                        return Results.NotFound(
                           ResponseHelper<string>.Error(
                               message: "AddressType Not Found",
                               statusCode: StatusCode.NOT_FOUND
                           ).ToDictionary()
                       );
                    }

                    return Results.Ok(
                       ResponseHelper<AddressTypeDeleteResponseDto>.Success(
                           message: "AddressType Deleted Successfully"
                       ).ToDictionary()
                   );
                }
                catch (Exception ex)
                {
                    return Results.Json(
                        ResponseHelper<string>.Error(
                            message: "An Unexpected Error occurred while Deleting the AddressType.",
                            exception: ex,
                            isWarning: false,
                            statusCode: StatusCode.INTERNAL_SERVER_ERROR
                        ).ToDictionary()
                    );
                }
            }).WithTags("AddressType")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Deletes a AddressType. ", description: "This endpoint allows you to delete a AddressType based on the provided AddressType Id."
            ));
        }
    }
}
