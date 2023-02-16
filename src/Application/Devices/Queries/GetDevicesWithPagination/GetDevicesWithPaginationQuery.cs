using Application.Common.DTOs;
using Application.Common.Helpers;
using MediatR;

namespace Application.Devices.Queries.GetDevicesWithPagination
{
    public sealed class GetDevicesWithPaginationQuery : IRequest<PagedList<DeviceDTO>>
    {
        public DeviceParameters? DeviceParameters { get; init; }
    }
}
