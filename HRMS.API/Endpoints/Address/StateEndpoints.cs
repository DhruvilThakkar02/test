using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.Address.State.StateRequestDtos;
using HRMS.Dtos.Address.State.StateResponseDtos;
using HRMS.Utility.Helpers.Enums;
using HRMS.Utility.Helpers.Handlers;
using HRMS.Utility.Helpers.LogHelpers.Interface;
using HRMS.Utility.Validators.Address.State;
using Swashbuckle.AspNetCore.Annotations;
using Newtonsoft.Json;
using Serilog;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Endpoints.Address
{
    public static class StateEndpoints
    {
            public static void MapStateEndpoints(this IEndpointRouteBuilder app)
            {
                app.MapGet("/state/getall", async (IStateService service, IStateLogger logger) =>
                {
                    var requestJson = JsonConvert.SerializeObject(new { service });
                    logger.LogInformation("Received request: {RequestJson}", requestJson);

                    logger.LogInformation("Fetching all States.");

                    var states = await service.GetStates();
                    if (states != null && states.Any())
                    {
                        var response = ResponseHelper<List<StateReadResponseDto>>.Success("States Retrieved Successfully", states.ToList());
                        logger.LogInformation("Successfully retrieved {Count} States.", states.Count);
                        return Results.Ok(response.ToDictionary());
                    }

                    logger.LogWarning("No States found.");
                    var errorResponse = ResponseHelper<List<StateReadResponseDto>>.Error("No States Found");
                    return Results.NotFound(errorResponse.ToDictionary());
                }).WithTags("State")
                .WithMetadata(new SwaggerOperationAttribute(summary: "Retrieves a List of States", description: "This endpoint returns a List of States. If no States are found, a 404 status code is returned."));

                // **GET /GetStateById/{id}** - Retrieve State by ID
                app.MapGet("/state/{id}", async (IStateService service, int id, IStateLogger logger) =>
                {
                    var requestJson = JsonConvert.SerializeObject(new { id });
                    logger.LogInformation("Received request: {RequestJson}", requestJson);

                    logger.LogInformation("Fetching State with Id {StateId}.", id);

                    var validator = new StateReadRequestValidator();
                    var stateRequestDto = new StateReadRequestDto { StateId = id };

                    var validationResult = validator.Validate(stateRequestDto);
                    if (!validationResult.IsValid)
                    {
                        var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                        logger.LogWarning("Validation failed for State with Id {StateId}: {Errors}", id, string.Join(", ", errorMessages));
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
                        var state = await service.GetStateById(id);
                        if (state == null)
                        {
                            logger.LogWarning("State with Id {StateId} not found.", id);
                            return Results.NotFound(
                                ResponseHelper<string>.Error(
                                    message: "State Not Found",
                                    statusCode: StatusCode.NOT_FOUND
                                ).ToDictionary()
                            );
                        }

                        logger.LogInformation("Successfully retrieved State with Id {StateId}.", id);
                        return Results.Ok(
                            ResponseHelper<StateReadResponseDto>.Success(
                                message: "State Retrieved Successfully",
                                data: state
                            ).ToDictionary()
                        );
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An unexpected error occurred while retrieving the State with Id {StateId}.", id);
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
                }).WithTags("State")
                .WithMetadata(new SwaggerOperationAttribute(summary: "Retrieve State by Id", description: "This endpoint returns State by Id. If no State is found, a 404 status code is returned."));

                
                app.MapPost("/state/create", async (StateCreateRequestDto dto, IStateService _stateService, IStateLogger logger) =>
                {
                    var requestJson = JsonConvert.SerializeObject(dto);
                    logger.LogInformation("Received request: {RequestJson}", requestJson);

                    logger.LogInformation("Creating new State with data: {StateData}", dto);

                    var validator = new StateCreateRequestValidator();
                    var validationResult = validator.Validate(dto);

                    if (!validationResult.IsValid)
                    {
                        var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                        logger.LogWarning("Validation failed for creating state: {Errors}", string.Join(", ", errorMessages));
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
                        var newState = await _stateService.CreateState(dto);
                        logger.LogInformation("Successfully created State with Id {StateId}.", newState.StateId);
                        return Results.Ok(
                            ResponseHelper<StateCreateResponseDto>.Success(
                                message: "State Created Successfully",
                                data: newState
                            ).ToDictionary()
                        );
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An unexpected error occurred while creating the State.");
                        return Results.Json(
                            ResponseHelper<string>.Error(
                                message: "An Unexpected Error occurred while Creating the State.",
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
                }).WithTags("State")
                .WithMetadata(new SwaggerOperationAttribute(summary: "Creates a new State", description: "This endpoint allows you to create a new State with the provided details."));

                
                app.MapPut("/state/update", async (IStateService service, [FromBody] StateUpdateRequestDto dto, IStateLogger logger) =>
                {
                    var requestJson = JsonConvert.SerializeObject(dto);
                    logger.LogInformation("Received request: {RequestJson}", requestJson);

                    logger.LogInformation("Updating State with ID {StateId}.", dto.StateId);

                    var validator = new StateUpdateRequestValidator();
                    var validationResult = validator.Validate(dto);

                    if (!validationResult.IsValid)
                    {
                        var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                        logger.LogWarning("Validation failed for updating State with Id {StateId}: {Errors}", dto.StateId, string.Join(", ", errorMessages));
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
                        var updatedState = await service.UpdateState(dto);
                        if (updatedState == null)
                        {
                            logger.LogWarning("State with Id {StateId} not found for update.", dto.StateId);
                            return Results.NotFound(
                               ResponseHelper<string>.Error(
                                   message: "State Not Found",
                                   statusCode: StatusCode.NOT_FOUND
                               ).ToDictionary()
                           );
                        }

                        logger.LogInformation("Successfully updated State with Id {StateId}.", dto.StateId);
                        return Results.Ok(
                            ResponseHelper<StateUpdateResponseDto>.Success(
                                message: "State Updated Successfully",
                                data: updatedState
                            ).ToDictionary()
                        );
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An unexpected error occurred while updating the State with Id {StateId}.", dto.StateId);
                        return Results.Json(
                            ResponseHelper<string>.Error(
                                message: "An Unexpected Error occurred while Updating the State.",
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
                }).WithTags("State")
                .WithMetadata(new SwaggerOperationAttribute(summary: "Updates existing State details", description: "This endpoint allows you to update State details with the provided Id."));

                
                app.MapDelete("/state/delete", async (IStateService service, [FromBody] StateDeleteRequestDto dto, IStateLogger logger) =>
                {
                    var requestJson = JsonConvert.SerializeObject(dto);
                    logger.LogInformation("Received request: {RequestJson}", requestJson);

                    logger.LogInformation("Deleting State with Id {StateId}.", dto.StateId);

                    var validator = new StateDeleteRequestValidator();
                    var validationResult = validator.Validate(dto);

                    if (!validationResult.IsValid)
                    {
                        var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                        logger.LogWarning("Validation failed for deleting State with Id {StateId}: {Errors}", dto.StateId, string.Join(", ", errorMessages));
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
                        var result = await service.DeleteState(dto);
                        if (result == null)
                        {
                            logger.LogWarning("State with ID {StateId} not found for deletion.", dto.StateId);
                            return Results.NotFound(
                               ResponseHelper<string>.Error(
                                   message: "State Not Found",
                                   statusCode: StatusCode.NOT_FOUND
                               ).ToDictionary()
                           );
                        }

                        logger.LogInformation("Successfully deleted State with Id {StateId}.", dto.StateId);
                        return Results.Ok(
                           ResponseHelper<StateDeleteResponseDto>.Success(
                               message: "State Deleted Successfully",
                               data: result
                           ).ToDictionary()
                       );
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An unexpected error occurred while deleting the State with Id {StateId}.", dto.StateId);
                        return Results.Json(
                            ResponseHelper<string>.Error(
                                message: "An Unexpected Error occurred while Deleting the State.",
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
                }).WithTags("State")
                .WithMetadata(new SwaggerOperationAttribute(summary: "Deletes a State", description: "This endpoint allows you to delete a State based on the provided State Id."));
            }
        }

    }

