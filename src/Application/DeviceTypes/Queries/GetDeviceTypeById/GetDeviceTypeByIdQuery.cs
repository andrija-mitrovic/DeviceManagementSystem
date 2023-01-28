using Application.Common.DTOs;
using MediatR;

namespace Application.DeviceTypes.Queries.GetDeviceTypeById
{
    public sealed class GetDeviceTypeByIdQuery : IRequest<DeviceTypeDetailDTO>
    {
        public int Id { get; init; }
    }
}
