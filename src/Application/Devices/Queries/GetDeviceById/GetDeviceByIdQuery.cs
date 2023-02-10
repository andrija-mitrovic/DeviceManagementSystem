using Application.Common.DTOs;
using MediatR;

namespace Application.Devices.Queries.GetDeviceById
{
    public sealed class GetDeviceByIdQuery : IRequest<DeviceDetailInfoDTO>
    {
        public int Id { get; init; }
    }
}
