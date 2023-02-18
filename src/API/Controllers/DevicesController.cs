using API.Filters;
using Application.Common.DTOs;
using Application.Common.Helpers;
using Application.Devices.Commands.CreateUpdateDevice;
using Application.Devices.Commands.DeleteDevice;
using Application.Devices.Queries.GetDeviceById;
using Application.Devices.Queries.GetDevicesWithPagination;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public sealed class DevicesController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<DeviceDTO>>> GetProductsWithPagination([FromQuery] DeviceParameters deviceParameters, CancellationToken cancellationToken)
        {
            var query = new GetDevicesWithPaginationQuery() { DeviceParameters = deviceParameters };

            var pagedResult = await Mediator.Send(query, cancellationToken);

            Response.AddPaginationHeader(pagedResult.MetaData);

            return pagedResult;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DeviceDetailInfoDTO>> GetDevice(int id, CancellationToken cancellationToken)
        {
            return await Mediator.Send(new GetDeviceByIdQuery() { Id = id }, cancellationToken);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<int>> CreateOrUpdateProduct(CreateUpdateDeviceCommand command, CancellationToken cancellationToken)
        {
            return await Mediator.Send(command, cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDevice(int id, CancellationToken cancellationToken)
        {
            await Mediator.Send(new DeleteDeviceCommand() { Id = id }, cancellationToken);

            return NoContent();
        }
    }
}
