using Application.Common.DTOs;
using MediatR;

namespace Application.Devices.Queries.GetDevices
{
    public sealed class GetDevicesQuery : IRequest<List<DeviceDTO>>
    {
    }
}
