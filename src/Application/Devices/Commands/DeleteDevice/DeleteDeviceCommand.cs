using MediatR;

namespace Application.Devices.Commands.DeleteDevice
{
    public sealed class DeleteDeviceCommand : IRequest<Unit>
    {
        public int Id { get; init; }
    }
}
