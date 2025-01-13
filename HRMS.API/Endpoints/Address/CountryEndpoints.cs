using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.Address.Country.CountryRequestDtos;
using HRMS.Dtos.Address.Country.CountryResponseDtos;
using HRMS.Utility.Helpers.Enums;
using HRMS.Utility.Helpers.Handlers;
using HRMS.Utility.Helpers.LogHelpers.Interface;
using HRMS.Utility.Validators.Address.Country;
using Swashbuckle.AspNetCore.Annotations;
using Newtonsoft.Json;
using Serilog;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Endpoints.Address
{
    public static class CountryEndpoints
    {
            public static void MapCountryEndpoints(this IEndpointRouteBuilder app)
            {
                
                app.MapGet("/Country/getall", async (ICountryService service, ICountryLogger logger) =>
                {
                    var requestJson = JsonConvert.SerializeObject(new { service });
                    logger.LogInformation("Received request: {RequestJson}", requestJson);

                    logger.LogInformation("Fetching all Countries.");

                    var countries = await service.GetCountries();
                    if (countries != null && countries.Any())
                    {
                        var response = ResponseHelper<List<CountryReadResponseDto>>.Success("Countries Retrieved Successfully", countries.ToList());
                        logger.LogInformation("Successfully retrieved {Count} Countries.", countries.Count);
                        return Results.Ok(response.ToDictionary());
                    }

                    logger.LogWarning("No Countries found.");
                    var errorResponse = ResponseHelper<IEnumerable<CountryReadResponseDto>>.Error("No Countries Found");
                    return Results.NotFound(errorResponse.ToDictionary());
                }).WithTags("Country")
                .WithMetadata(new SwaggerOperationAttribute(summary: "Retrieves a List of Countries", description: "This endpoint returns a List of Countries. If no Countries are found, a 404 status code is returned."));

                
                app.MapGet("/Country/{id}", async (ICountryService service,int id, ICountryLogger logger) =>
                {
                    var requestJson = JsonConvert.SerializeObject(new { id });
                    logger.LogInformation("Received request: {RequestJson}", requestJson);

                    logger.LogInformation("Fetching Country with Id {CountryId}.", id);

                    var validator = new CountryReadRequestValidator();
                    var countryRequestDto = new CountryReadRequestDto { CountryId = id };

                    var validationResult = validator.Validate(countryRequestDto);
                    if (!validationResult.IsValid)
                    {
                        var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                        logger.LogWarning("Validation failed for Country with Id {CountryId}: {Errors}", id, string.Join(", ", errorMessages));
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
                        var country = await service.GetCountryById(id);
                        if (country == null)
                        {
                            logger.LogWarning("Country with Id {CountryId} not found.", id);
                            return Results.NotFound(
                                ResponseHelper<string>.Error(
                                    message: "Country Not Found",
                                    statusCode: StatusCode.NOT_FOUND
                                ).ToDictionary()
                            );
                        }

                        logger.LogInformation("Successfully retrieved Country with Id {CountryId}.", id);
                        return Results.Ok(
                            ResponseHelper<CountryReadResponseDto>.Success(
                                message: "Country Retrieved Successfully",
                                data: country
                            ).ToDictionary()
                        );
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An unexpected error occurred while retrieving the Country with Id {CountryId}.", id);
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
                }).WithTags("Country")
                .WithMetadata(new SwaggerOperationAttribute(summary: "Retrieve Country by Id", description: "This endpoint returns a Country by Id. If no Country is found, a 404 status code is returned."));

                
                app.MapPost("/country/create", async (CountryCreateRequestDto dto, ICountryService _countryService, ICountryLogger logger) =>
                {
                    var requestJson = JsonConvert.SerializeObject(dto);
                    logger.LogInformation("Received request: {RequestJson}", requestJson);

                    logger.LogInformation("Creating new Country with data: {CountryData}", dto);

                    var validator = new CountryCreateRequestValidator();
                    var validationResult = validator.Validate(dto);

                    if (!validationResult.IsValid)
                    {
                        var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                        logger.LogWarning("Validation failed for creating country: {Errors}", string.Join(", ", errorMessages));
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
                        var newCountry = await _countryService.CreateCountry(dto);
                        logger.LogInformation("Successfully created Country with Id {CountryId}.", newCountry.CountryId);
                        return Results.Ok(
                            ResponseHelper<CountryCreateResponseDto>.Success(
                                message: "Country Created Successfully",
                                data: newCountry
                            ).ToDictionary()
                        );
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An unexpected error occurred while creating the Country.");
                        return Results.Json(
                            ResponseHelper<string>.Error(
                                message: "An Unexpected Error occurred while Creating the Country.",
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
                }).WithTags("Country")
                .WithMetadata(new SwaggerOperationAttribute(summary: "Creates a new Country", description: "This endpoint allows you to create a new Country with the provided details."));

               
                app.MapPut("/country/update", async (ICountryService service, [FromBody] CountryUpdateRequestDto dto, ICountryLogger logger) =>
                {
                    var requestJson = JsonConvert.SerializeObject(dto);
                    logger.LogInformation("Received request: {RequestJson}", requestJson);

                    logger.LogInformation("Updating Country with ID {CountryId}.", dto.CountryId);

                    var validator = new CountryUpdateRequestValidator();
                    var validationResult = validator.Validate(dto);

                    if (!validationResult.IsValid)
                    {
                        var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                        logger.LogWarning("Validation failed for updating Country with Id {CountryId}: {Errors}", dto.CountryId, string.Join(", ", errorMessages));
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
                        var updatedCountry = await service.UpdateCountry(dto);
                        if (updatedCountry == null)
                        {
                            logger.LogWarning("Country with Id {CountryId} not found for update.", dto.CountryId);
                            return Results.NotFound(
                               ResponseHelper<string>.Error(
                                   message: "Country Not Found",
                                   statusCode: StatusCode.NOT_FOUND
                               ).ToDictionary()
                           );
                        }

                        logger.LogInformation("Successfully updated Country with Id {CountryId}.", dto.CountryId);
                        return Results.Ok(
                            ResponseHelper<CountryUpdateResponseDto>.Success(
                                message: "Country Updated Successfully",
                                data: updatedCountry
                            ).ToDictionary()
                        );
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An unexpected error occurred while updating the Country with Id {CountryId}.", dto.CountryId);
                        return Results.Json(
                            ResponseHelper<string>.Error(
                                message: "An Unexpected Error occurred while Updating the Country.",
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
                }).WithTags("Country")
                .WithMetadata(new SwaggerOperationAttribute(summary: "Updates existing Country details", description: "This endpoint allows you to update Country details with the provided Id."));

                
                app.MapDelete("/country/delete", async (ICountryService service, [FromBody] CountryDeleteRequestDto dto, ICountryLogger logger) =>
                {
                    var requestJson = JsonConvert.SerializeObject(dto);
                    logger.LogInformation("Received request: {RequestJson}", requestJson);

                    logger.LogInformation("Deleting Country with Id {CountryId}.", dto.CountryId);

                    var validator = new CountryDeleteRequestValidator();
                    var validationResult = validator.Validate(dto);

                    if (!validationResult.IsValid)
                    {
                        var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                        logger.LogWarning("Validation failed for deleting Country with Id {CountryId}: {Errors}", dto.CountryId, string.Join(", ", errorMessages));
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
                        var result = await service.DeleteCountry(dto);
                        if (result == null)
                        {
                            logger.LogWarning("Country with ID {CountryId} not found for deletion.", dto.CountryId);
                            return Results.NotFound(
                               ResponseHelper<string>.Error(
                                   message: "Country Not Found",
                                   statusCode: StatusCode.NOT_FOUND
                               ).ToDictionary()
                           );
                        }

                        logger.LogInformation("Successfully deleted Country with Id {CountryId}.", dto.CountryId);
                        return Results.Ok(
                           ResponseHelper<CountryDeleteResponseDto>.Success(
                               message: "Country Deleted Successfully",
                               data: result
                           ).ToDictionary()
                       );
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An unexpected error occurred while deleting the Country with Id {CountryId}.", dto.CountryId);
                        return Results.Json(
                            ResponseHelper<string>.Error(
                                message: "An Unexpected Error occurred while Deleting the Country.",
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
                }).WithTags("Country")
                .WithMetadata(new SwaggerOperationAttribute(summary: "Deletes a Country", description: "This endpoint allows you to delete a Country based on the provided Country Id."));
            }
        }

    }

