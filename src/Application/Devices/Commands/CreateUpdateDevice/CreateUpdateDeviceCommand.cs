using Application.Common.DTOs;
using MediatR;

namespace Application.Devices.Commands.CreateUpdateDevice
{
    public sealed class CreateUpdateDeviceCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int DeviceTypeId { get; set; }
        public List<CreateDevicePropertyValueDTO>? DevicePropertyValues { get; init; }
    }
}
