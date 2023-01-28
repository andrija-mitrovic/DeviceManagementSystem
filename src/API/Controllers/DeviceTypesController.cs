using Application.Common.DTOs;
using Application.DeviceTypes.Commands.CreateDeviceType;
using Application.DeviceTypes.Commands.DeleteDeviceType;
using Application.DeviceTypes.Queries.GetDeviceTypeById;
using Application.DeviceTypes.Queries.GetDeviceTypes;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public sealed class DeviceTypesController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<DeviceTypeDTO>>> GetDeviceTypes(CancellationToken cancellationToken)
        {
            return await Mediator.Send(new GetDeviceTypesQuery(), cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DeviceTypeDetailDTO>> GetDeviceType(int id, CancellationToken cancellationToken)
        {
            return await Mediator.Send(new GetDeviceTypeByIdQuery() { Id = id }, cancellationToken);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateProduct(CreateDeviceTypeCommand command, CancellationToken cancellationToken)
        {
            return await Mediator.Send(command, cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDeviceType(int id, CancellationToken cancellationToken)
        {
            await Mediator.Send(new DeleteDeviceTypeCommand() { Id = id }, cancellationToken);

            return NoContent();
        }
    }
}
