#nullable disable

using Azure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class BaseController : ControllerBase
{
    // Shared Dependencies
    // Logger, CQRS Pattern, MediatR

    private ISender _mediator;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    /// <summary>
    /// Sends a request using MediatR and returns a standardized response.
    /// </summary>
    /// <typeparam name="T">The type of the data in the response.</typeparam>
    /// <param name="request">The request to be sent.</param>
    /// <param name="ct">Optional cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation, containing the ObjectResult.</returns>
    private async Task<ObjectResult> Send<T>(IRequest<Response<T>> request, CancellationToken ct = default)
    {
        var result = await Mediator.Send(request, ct);

        // Returns OK regardless of success or failure, could be modified to handle errors differently
        return Ok(result);
    }

    /// <summary>
    /// Overloaded method for sending requests returning a response with object type.
    /// </summary>
    protected Task<ObjectResult> Send(IRequest<Response<object>> request, CancellationToken ct = default)
        => Send<object>(request, ct);

    /// <summary>
    /// Overloaded method for sending requests returning a response with Guid type.
    /// </summary>
    protected Task<ObjectResult> Send(IRequest<Response<Guid>> request, CancellationToken ct = default)
        => Send<Guid>(request, ct);

    /// <summary>
    /// Overloaded method for sending requests returning a response with int type.
    /// </summary>
    protected Task<ObjectResult> Send(IRequest<Response<int>> request, CancellationToken ct = default)
        => Send<int>(request, ct);

    /// <summary>
    /// Overloaded method for sending requests returning a response with long type.
    /// </summary>
    protected Task<ObjectResult> Send(IRequest<Response<long>> request, CancellationToken ct = default)
        => Send<long>(request, ct);
}
