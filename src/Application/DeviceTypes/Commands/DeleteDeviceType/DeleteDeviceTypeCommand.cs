using MediatR;

namespace Application.DeviceTypes.Commands.DeleteDeviceType
{
    public sealed class DeleteDeviceTypeCommand : IRequest<Unit>
    {
        public int Id { get; init; }
    }
}
