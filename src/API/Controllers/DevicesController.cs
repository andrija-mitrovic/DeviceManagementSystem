using Application.Devices.Commands.CreateDevice;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public sealed class DevicesController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<int>> CreateProduct(CreateDeviceCommand command, CancellationToken cancellationToken)
        {
            return await Mediator.Send(command, cancellationToken);
        }
    }
}
