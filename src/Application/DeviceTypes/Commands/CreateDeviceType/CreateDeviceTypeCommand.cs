using Application.Common.DTOs;
using MediatR;

namespace Application.DeviceTypes.Commands.CreateDeviceType
{
    public sealed class CreateDeviceTypeCommand : IRequest<int>
    {
        public int Id { get; init; }
        public string? Name { get; init; }
        public List<CreateDeviceTypePropertyDTO>? DeviceTypeProperties { get; init; } 
    }
}
