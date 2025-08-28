using MediatR;
using Microsoft.AspNetCore.Mvc;
using MIOTO.API.Abstractions;
using MIOTO.APPLICATION.Features.Identities.AppRoles;
using MIOTO.APPLICATION.Models.Requests;

namespace MIOTO.API.Controllers;

public class AuthController : ApiController
{
    public AuthController(ISender sender) : base(sender)
    {
    }
    
    [HttpPost("create-role")]
    public async Task<IActionResult> CreateRole([FromBody] CreateAppRoleRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateAppRoleCommand(
            request.Name,
            request.RoleCode,
            request.Description
        );

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(new { errors = result.Error });
    }
    
    
}