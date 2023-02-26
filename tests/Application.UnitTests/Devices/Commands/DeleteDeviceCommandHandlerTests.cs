using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Devices.Commands.DeleteDevice;
using Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Application.UnitTests.Devices.Commands
{
    public sealed class DeleteDeviceCommandHandlerTests
    {
        private readonly Mock<IDeviceRepository> _deviceRepositoryMock;
        private readonly Mock<IDevicePropertyValueRepository> _devicePropertyValueRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ILogger<DeleteDeviceCommandHandler>> _loggerMock;

        public DeleteDeviceCommandHandlerTests()
        {
            _deviceRepositoryMock = new();
            _devicePropertyValueRepositoryMock = new();
            _unitOfWorkMock = new();
            _loggerMock = new();
        }

        [Fact]
        public async Task Handle_ShouldDeleteDevice_WhenDeleteRequestIsValid()
        {
            //Arrange
            var command = ReturnDeleteDeviceCommand();
            var device = ReturnDevice();

            var handler = new DeleteDeviceCommandHandler(
                _deviceRepositoryMock.Object,
                _devicePropertyValueRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _loggerMock.Object);

            _deviceRepositoryMock.Setup(x =>
                x.GetDeviceWithDevicePropertyValueById(It.IsAny<int>(), false, default))
                    .ReturnsAsync(device);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task Handle_ShouldThrowDeviceNotFoundException_WhenDeviceDoesntExist()
        {
            //Arrange
            var command = ReturnDeleteDeviceCommand();

            var handler = new DeleteDeviceCommandHandler(
                _deviceRepositoryMock.Object,
                _devicePropertyValueRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _loggerMock.Object);

            _deviceRepositoryMock.Setup(x =>
                x.GetDeviceWithDevicePropertyValueById(It.IsAny<int>(), true, default))
                    .ReturnsAsync(value: null);

            //Act
            var result = async () => await handler.Handle(command, default);

            //Assert
            await result.Should().ThrowAsync<DeviceNotFoundException>();
        }

        private static DeleteDeviceCommand ReturnDeleteDeviceCommand() =>
            new()
            {
                Id = 1
            };

        private static Device ReturnDevice() =>
            new()
            {
                Id = 1,
                Name = "Dell Inspirion 15000"
            };
    }
}
