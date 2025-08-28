using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MIOTO.API.Abstractions;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiController : ControllerBase
{
    protected readonly ISender Sender;

    protected ApiController(ISender sender)
    {
        Sender = sender;
    }

    protected ApiController()
    {
    }
}