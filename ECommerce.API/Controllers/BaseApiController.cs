using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[ApiController]
[Produces("application/json")]
public abstract class BaseApiController : ControllerBase
{
    protected readonly ISender Sender;

    protected BaseApiController(ISender sender)
    {
        Sender = sender;
    }
}