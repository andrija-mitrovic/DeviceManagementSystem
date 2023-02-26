using Application.Common.Interfaces;
using Application.Devices.Commands.CreateUpdateDevice;
using AutoMapper;
using Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Application.UnitTests.Devices.Commands
{
    public sealed class CreateUpdateDeviceCommandHandlerTests
    {
        private readonly Mock<IDeviceRepository> _deviceRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<CreateUpdateDeviceCommandHandler>> _loggerMock;

        public CreateUpdateDeviceCommandHandlerTests()
        {
            _deviceRepositoryMock = new();
            _unitOfWorkMock = new();
            _mapperMock = new();
            _loggerMock = new();
        }

        [Fact]
        public async Task Handle_ShouldCreateUpdateDevice_WhenCreateUpdateRequestIsValid()
        {
            //Arrange
            var command = ReturnCreateUpdateDeviceCommand();
            var device = ReturnDevice();

            var handler = new CreateUpdateDeviceCommandHandler(
                _deviceRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _mapperMock.Object,
                _loggerMock.Object);

            _mapperMock.Setup(x => x.Map<Device>(command)).Returns(device);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.Should().BePositive();
            result.Should().NotBe(null);
        }

        [Fact]
        public async Task Handle_ShouldCallAddOnRepository_WhenCreateUpdateRequestIsValid()
        {
            //Arrange
            var command = ReturnCreateUpdateDeviceCommand();
            var device = ReturnDevice();

            var handler = new CreateUpdateDeviceCommandHandler(
                _deviceRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _mapperMock.Object,
                _loggerMock.Object);

            _mapperMock.Setup(x => x.Map<Device>(command)).Returns(device);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            _deviceRepositoryMock.Verify(x => x.AddAsync(It.Is<Device>(y => y.Id == result), default), Times.Once());
        }

        private static CreateUpdateDeviceCommand ReturnCreateUpdateDeviceCommand() =>
            new()
            {
                Name = "Dell Inspirion 15000"
            };

        private static Device ReturnDevice() =>
            new()
            {
                Id = 1,
                Name = "Dell Inspirion 15000"
            };
    }
}
