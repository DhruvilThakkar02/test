using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.Address.City.CityRequestDtos;
using HRMS.Dtos.Address.City.CityResponseDtos;
using HRMS.Utility.Helpers.Enums;
using HRMS.Utility.Helpers.Handlers;
using HRMS.Utility.Helpers.LogHelpers.Interface;
using HRMS.Utility.Validators.Address.City;
using Swashbuckle.AspNetCore.Annotations;
using Newtonsoft.Json;
using Serilog;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Endpoints.Address
{
    public static class CityEndpoints
    {
            public static void MapCityEndpoints(this IEndpointRouteBuilder app)
            {
                app.MapGet("/city/getall", async (ICityService service, ICityLogger logger) =>
                {
                    var requestJson = JsonConvert.SerializeObject(new { service });
                    logger.LogInformation("Received request: {RequestJson}", requestJson);

                    logger.LogInformation("Fetching all Cities.");

                    var cities = await service.GetCities();
                    if (cities != null && cities.Any())
                    {
                        var response = ResponseHelper<List<CityReadResponseDto>>.Success("Cities Retrieved Successfully", cities.ToList());
                        logger.LogInformation("Successfully retrieved {Count} Cities.", cities.Count);
                        return Results.Ok(response.ToDictionary());
                    }

                    logger.LogWarning("No Cities found.");
                    var errorResponse = ResponseHelper<IEnumerable<CityReadResponseDto>>.Error("No Cities Found");
                    return Results.NotFound(errorResponse.ToDictionary());
                }).WithTags("City")
                .WithMetadata(new SwaggerOperationAttribute(summary: "Retrieves a List of Cities", description: "This endpoint returns a List of Cities. If no Cities are found, a 404 status code is returned."));

                
                app.MapGet("/city/{id}", async (ICityService service, int id, ICityLogger logger) =>
                {
                    var requestJson = JsonConvert.SerializeObject(new { id });
                    logger.LogInformation("Received request: {RequestJson}", requestJson);

                    logger.LogInformation("Fetching City with Id {CityId}.", id);

                    var validator = new CityReadRequestValidator();
                    var cityRequestDto = new CityReadRequestDto { CityId = id };

                    var validationResult = validator.Validate(cityRequestDto);
                    if (!validationResult.IsValid)
                    {
                        var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                        logger.LogWarning("Validation failed for City with Id {CityId}: {Errors}", id, string.Join(", ", errorMessages));
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
                        var city = await service.GetCityById(id);
                        if (city == null)
                        {
                            logger.LogWarning("City with Id {CityId} not found.", id);
                            return Results.NotFound(
                                ResponseHelper<string>.Error(
                                    message: "City Not Found",
                                    statusCode: StatusCodeEnum.NOT_FOUND
                                ).ToDictionary()
                            );
                        }

                        logger.LogInformation("Successfully retrieved City with Id {CityId}.", id);
                        return Results.Ok(
                            ResponseHelper<CityReadResponseDto>.Success(
                                message: "City Retrieved Successfully",
                                data: city
                            ).ToDictionary()
                        );
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An unexpected error occurred while retrieving the City with Id {CityId}.", id);
                        return Results.Json(
                            ResponseHelper<string>.Error(
                                message: "An Unexpected Error occurred.",
                                exception: ex,
                                isWarning: false,
                                statusCode: StatusCodeEnum.INTERNAL_SERVER_ERROR
                            ).ToDictionary()
                        );
                    }
                    finally
                    {
                        Log.CloseAndFlush();
                    }
                }).WithTags("City")
                .WithMetadata(new SwaggerOperationAttribute(summary: "Retrieve City by Id", description: "This endpoint returns a City by Id. If no City is found, a 404 status code is returned."));

                // **POST /CreateCity** - Create a new city
                app.MapPost("/city/create", async (CityCreateRequestDto dto, ICityService _cityService, ICityLogger logger) =>
                {
                    var requestJson = JsonConvert.SerializeObject(dto);
                    logger.LogInformation("Received request: {RequestJson}", requestJson);

                    logger.LogInformation("Creating new City with data: {CityData}", dto);

                    var validator = new CityCreateRequestValidator();
                    var validationResult = validator.Validate(dto);

                    if (!validationResult.IsValid)
                    {
                        var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                        logger.LogWarning("Validation failed for creating city: {Errors}", string.Join(", ", errorMessages));
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
                        var newCity = await _cityService.CreateCity(dto);
                        logger.LogInformation("Successfully created City with Id {CityId}.", newCity.CityId);
                        return Results.Ok(
                            ResponseHelper<CityCreateResponseDto>.Success(
                                message: "City Created Successfully",
                                data: newCity
                            ).ToDictionary()
                        );
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An unexpected error occurred while creating the City.");
                        return Results.Json(
                            ResponseHelper<string>.Error(
                                message: "An Unexpected Error occurred while Creating the City.",
                                exception: ex,
                                isWarning: false,
                                statusCode: StatusCodeEnum.INTERNAL_SERVER_ERROR
                            ).ToDictionary()
                        );
                    }
                    finally
                    {
                        Log.CloseAndFlush();
                    }
                }).WithTags("City")
                .WithMetadata(new SwaggerOperationAttribute(summary: "Creates a new City", description: "This endpoint allows you to create a new City with the provided details."));

                app.MapPut("/city/update", async (ICityService service, [FromBody] CityUpdateRequestDto dto, ICityLogger logger) =>
                {
                    var requestJson = JsonConvert.SerializeObject(dto);
                    logger.LogInformation("Received request: {RequestJson}", requestJson);

                    logger.LogInformation("Updating City with ID {CityId}.", dto.CityId);

                    var validator = new CityUpdateRequestValidator();
                    var validationResult = validator.Validate(dto);

                    if (!validationResult.IsValid)
                    {
                        var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                        logger.LogWarning("Validation failed for updating City with Id {CityId}: {Errors}", dto.CityId, string.Join(", ", errorMessages));
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
                        var updatedCity = await service.UpdateCity(dto);
                        if (updatedCity == null)
                        {
                            logger.LogWarning("City with Id {CityId} not found for update.", dto.CityId);
                            return Results.NotFound(
                               ResponseHelper<string>.Error(
                                   message: "City Not Found",
                                   statusCode: StatusCodeEnum.NOT_FOUND
                               ).ToDictionary()
                           );
                        }

                        logger.LogInformation("Successfully updated City with Id {CityId}.", dto.CityId);
                        return Results.Ok(
                            ResponseHelper<CityUpdateResponseDto>.Success(
                                message: "City Updated Successfully",
                                data: updatedCity
                            ).ToDictionary()
                        );
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An unexpected error occurred while updating the City with Id {CityId}.", dto.CityId);
                        return Results.Json(
                            ResponseHelper<string>.Error(
                                message: "An Unexpected Error occurred while Updating the City.",
                                exception: ex,
                                isWarning: false,
                                statusCode: StatusCodeEnum.INTERNAL_SERVER_ERROR
                            ).ToDictionary()
                        );
                    }
                    finally
                    {
                        Log.CloseAndFlush();
                    }
                }).WithTags("City")
                .WithMetadata(new SwaggerOperationAttribute(summary: "Updates existing City details", description: "This endpoint allows you to update City details with the provided Id."));

                app.MapDelete("/city/delete", async (ICityService service, [FromBody] CityDeleteRequestDto dto, ICityLogger logger) =>
                {
                    var requestJson = JsonConvert.SerializeObject(dto);
                    logger.LogInformation("Received request: {RequestJson}", requestJson);

                    logger.LogInformation("Deleting City with Id {CityId}.", dto.CityId);

                    var validator = new CityDeleteRequestValidator();
                    var validationResult = validator.Validate(dto);

                    if (!validationResult.IsValid)
                    {
                        var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                        logger.LogWarning("Validation failed for deleting City with Id {CityId}: {Errors}", dto.CityId, string.Join(", ", errorMessages));
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
                        var result = await service.DeleteCity(dto);
                        if (result == null)
                        {
                            logger.LogWarning("City with ID {CityId} not found for deletion.", dto.CityId);
                            return Results.NotFound(
                               ResponseHelper<string>.Error(
                                   message: "City Not Found",
                                   statusCode: StatusCodeEnum.NOT_FOUND
                               ).ToDictionary()
                           );
                        }

                        logger.LogInformation("Successfully deleted City with Id {CityId}.", dto.CityId);
                        return Results.Ok(
                           ResponseHelper<CityDeleteResponseDto>.Success(
                               message: "City Deleted Successfully",
                               data: result
                           ).ToDictionary()
                       );
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An unexpected error occurred while deleting the City with Id {CityId}.", dto.CityId);
                        return Results.Json(
                            ResponseHelper<string>.Error(
                                message: "An Unexpected Error occurred while Deleting the City.",
                                exception: ex,
                                isWarning: false,
                                statusCode: StatusCodeEnum.INTERNAL_SERVER_ERROR
                            ).ToDictionary()
                        );
                    }
                    finally
                    {
                        Log.CloseAndFlush();
                    }
                }).WithTags("City")
                .WithMetadata(new SwaggerOperationAttribute(summary: "Deletes a City", description: "This endpoint allows you to delete a City based on the provided City Id."));
            }
    }
}


