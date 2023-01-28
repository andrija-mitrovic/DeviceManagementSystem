using FluentValidation;

namespace Application.Devices.Commands.CreateDevice
{
    internal sealed class CreateDeviceCommandValidator : AbstractValidator<CreateDeviceCommand>
    {
        public CreateDeviceCommandValidator()
        {
            //RuleFor(x => x.Name).NotEmpty();
            //RuleFor(x => x.DeviceType!.Name).NotEmpty();
        }
    }
}
