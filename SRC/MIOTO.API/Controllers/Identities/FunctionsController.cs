using MediatR;
using Microsoft.AspNetCore.Mvc;
using MIOTO.API.Abstractions;
using MIOTO.APPLICATION.Features.Identities.Functions;
using MIOTO.APPLICATION.Models.Requests.Funtions;

namespace MIOTO.API.Controllers;

public class FunctionsController : ApiController
{
    private readonly ISender _sender;

    public FunctionsController(ISender sender)
    {
        _sender = sender;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetFunctionsQuery(), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(new { errors = result.Error });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetFunctionByIdQuery(id), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : NotFound(new { errors = result.Error });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateFunctionRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateFunctionCommand(
            request.Name,
            request.Url,
            request.ParrentId,
            request.SortOrder,
            request.CssClass,
            request.IsActive
        );

        var result = await _sender.Send(command, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(new { errors = result.Error });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateFunctionRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateFunctionCommand(
            id,
            request.Name,
            request.Url,
            request.ParrentId,
            request.SortOrder,
            request.CssClass,
            request.IsActive,
            request.ActionInFunctions
        );

        var result = await _sender.Send(command, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(new { errors = result.Error });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new DeleteFunctionCommand(id), cancellationToken);
        return result.IsSuccess ? NoContent() : NotFound(new { errors = result.Error });
    }
}