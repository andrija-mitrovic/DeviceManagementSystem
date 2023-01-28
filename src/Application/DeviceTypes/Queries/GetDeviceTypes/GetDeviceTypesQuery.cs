using Application.Common.DTOs;
using MediatR;

namespace Application.DeviceTypes.Queries.GetDeviceTypes
{
    public sealed class GetDeviceTypesQuery : IRequest<List<DeviceTypeDTO>>
    {
    }
}
