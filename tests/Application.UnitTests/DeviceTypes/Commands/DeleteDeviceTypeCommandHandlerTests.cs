using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DeviceTypes.Commands.DeleteDeviceType;
using Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Application.UnitTests.DeviceTypes.Commands
{
    public sealed class DeleteDeviceTypeCommandHandlerTests
    {
        private readonly Mock<IDeviceTypeRepository> _deviceTypeRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ILogger<DeleteDeviceTypeCommandHandler>> _loggerMock;

        public DeleteDeviceTypeCommandHandlerTests()
        {
            _deviceTypeRepositoryMock = new();
            _unitOfWorkMock = new();
            _loggerMock = new();
        }

        [Fact]
        public async Task Handle_ShouldDeleteDeviceType_WhenDeleteRequestIsValid()
        {
            //Arrange
            var command = ReturnDeleteDeviceTypeCommand();
            var deviceType = ReturnDeviceType();

            var handler = new DeleteDeviceTypeCommandHandler(
                _deviceTypeRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _loggerMock.Object);

            _deviceTypeRepositoryMock.Setup(x => 
                x.GetDeviceTypeWithDeviceTypePropertyByIdAsync(It.IsAny<int>(), true, default))
                    .ReturnsAsync(deviceType);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task Handle_ShouldThrowDeviceTypeNotFoundException_WhenDeviceTypeDoesntExist()
        {
            //Arrange
            var command = ReturnDeleteDeviceTypeCommand();

            var handler = new DeleteDeviceTypeCommandHandler(
                _deviceTypeRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _loggerMock.Object);

            _deviceTypeRepositoryMock.Setup(x => 
                x.GetDeviceTypeWithDeviceTypePropertyByIdAsync(It.IsAny<int>(), true, default))
                    .ReturnsAsync(value: null);
            //Act
            Func<Task> result = async () => await handler.Handle(command, default);

            //Assert
            await result.Should().ThrowAsync<DeviceTypeNotFoundException>();
        }

        private static DeleteDeviceTypeCommand ReturnDeleteDeviceTypeCommand() =>
            new()
            {
                Id = 1,
            };

        private static DeviceType ReturnDeviceType() =>
            new()
            {
                Id = 1,
                Name = "Computer"
            };
    }
}
