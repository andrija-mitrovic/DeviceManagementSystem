using FluentValidation;

namespace Application.Devices.Commands.CreateUpdateDevice
{
    internal sealed class CreateUpdateDeviceCommandValidator : AbstractValidator<CreateUpdateDeviceCommand>
    {
        public CreateUpdateDeviceCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            //RuleFor(x => x.DeviceType!.Name).NotEmpty();
        }
    }
}
