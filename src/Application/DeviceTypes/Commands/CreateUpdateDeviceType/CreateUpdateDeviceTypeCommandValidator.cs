using FluentValidation;

namespace Application.DeviceTypes.Commands.CreateUpdateDeviceType
{
    internal sealed class CreateUpdateDeviceTypeCommandValidator : AbstractValidator<CreateUpdateDeviceTypeCommand>
    {
        public CreateUpdateDeviceTypeCommandValidator() 
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.DeviceTypeProperties).NotNull();
        }
    }
}
