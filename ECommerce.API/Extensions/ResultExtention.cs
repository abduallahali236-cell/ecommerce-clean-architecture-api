using ECommerce.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToActionResult<T>(
        this Result<T> result,
        ControllerBase controller)
    {
        if (result.IsSuccess)
            return controller.Ok(result.Value);

        var error = result.Errors.FirstOrDefault()
            ?? new Error(
                "Unknown",
                "Unknown error");

        return error.Type switch
        {
            ErrorType.Validation =>
                controller.BadRequest(result.Errors),

            ErrorType.NotFound =>
                controller.NotFound(result.Errors),

            ErrorType.Conflict =>
                controller.Conflict(result.Errors),

            ErrorType.Unauthorized =>
                controller.Unauthorized(result.Errors),

            ErrorType.Forbidden =>
                controller.StatusCode(StatusCodes.Status403Forbidden, result.Errors),

            _ =>
                controller.BadRequest(result.Errors)
        };
    }

    public static IActionResult ToActionResult(
        this Result result,
        ControllerBase controller)
    {
        if (result.IsSuccess)
            return controller.NoContent();

        var error = result.Errors.First();

        return error.Type switch
        {
            ErrorType.Validation =>
                controller.BadRequest(result.Errors),

            ErrorType.NotFound =>
                controller.NotFound(result.Errors),

            ErrorType.Conflict =>
                controller.Conflict(result.Errors),

            ErrorType.Unauthorized =>
                controller.Unauthorized(result.Errors),

            ErrorType.Forbidden =>
                controller.StatusCode(StatusCodes.Status403Forbidden, result.Errors),

            _ =>
                controller.BadRequest(result.Errors)
        };
    }
}