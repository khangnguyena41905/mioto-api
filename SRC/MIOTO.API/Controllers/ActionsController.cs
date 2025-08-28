using MediatR;
using Microsoft.AspNetCore.Mvc;
using MIOTO.API.Abstractions;
using MIOTO.APPLICATION.Features.Identities.Actions;
using MIOTO.APPLICATION.Models.Requests.Actions;

namespace MIOTO.API.Controllers;

public class ActionsController : ApiController
{
    private readonly ISender _sender;

    public ActionsController(ISender sender)
    {
        _sender = sender;
    }
    /// <summary>
    /// Lấy danh sách tất cả Actions
    /// GET: api/actions
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _sender.Send(new GetActionsQuery());

        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(new { errors = result.Error });
    }

    /// <summary>
    /// Lấy chi tiết Action theo Id
    /// GET: api/actions/{id}
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetActionByIdQuery(id), cancellationToken);

        if (result.IsSuccess)
            return Ok(result.Value);

        return NotFound(new { errors = result.Error });
    }

    /// <summary>
    /// Tạo mới Action
    /// POST: api/actions
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateActionRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateActionCommand(
            request.Name,
            request.SortOrder,
            request.IsActive
        );

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(new { errors = result.Error });
    }

    /// <summary>
    /// Cập nhật Action
    /// PUT: api/actions/{id}
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateActionRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateActionCommand(
            id,
            request.Name,
            request.SortOrder,
            request.IsActive
        );

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(new { errors = result.Error });
    }

    /// <summary>
    /// Xoá Action
    /// DELETE: api/actions/{id}
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new DeleteActionCommand(id), cancellationToken);

        if (result.IsSuccess)
            return NoContent();

        return NotFound(new { errors = result.Error });
    }
    
}