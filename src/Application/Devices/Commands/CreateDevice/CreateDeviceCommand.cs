using Application.Common.DTOs;
using MediatR;

namespace Application.Devices.Commands.CreateDevice
{
    public sealed class CreateDeviceCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public CreateDeviceTypePropertyDTO? DeviceTypeProperties { get; set; }
    }
}
