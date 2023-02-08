using Application.Common.DTOs;
using MediatR;

namespace Application.DeviceTypes.Commands.CreateUpdateDeviceType
{
    public sealed class CreateUpdateDeviceTypeCommand : IRequest<int>
    {
        public int Id { get; init; }
        public string? Name { get; init; }
        public int? ParentId { get; init; }
        public List<CreateDeviceTypePropertyDTO>? DeviceTypeProperties { get; init; } 
    }
}
