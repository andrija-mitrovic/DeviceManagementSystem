using Application.Common.Interfaces;
using Application.DeviceTypes.Commands.CreateUpdateDeviceType;
using AutoMapper;
using Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Application.UnitTests.DeviceTypes.Commands
{
    public sealed class CreateUpdateDeviceTypeCommandHandlerTests
    {
        private readonly Mock<IDeviceTypeRepository> _deviceTypeRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<CreateUpdateDeviceTypeCommandHandler>> _loggerMock;

        public CreateUpdateDeviceTypeCommandHandlerTests()
        {
            _deviceTypeRepositoryMock = new();
            _unitOfWorkMock = new();
            _mapperMock = new();
            _loggerMock = new();
        }

        [Fact]
        public async Task Handle_ShouldCreateUpdateDeviceType_WhenCreateUpdateRequestIsValid()
        {
            //Arrange
            var command = ReturnCreateUpdateDeviceTypeCommand();
            var deviceType = ReturnDeviceType();

            var handler = new CreateUpdateDeviceTypeCommandHandler(
                _deviceTypeRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _mapperMock.Object,
                _loggerMock.Object);

            _mapperMock.Setup(x => x.Map<DeviceType>(command)).Returns(deviceType);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.Should().BePositive();
            result.Should().NotBe(null);
        }

        [Fact]
        public async Task Handle_ShouldCallAddOnRepository_WhenCreateRequestIsValid()
        {
            //Arrange
            var command = ReturnCreateUpdateDeviceTypeCommand();
            var deviceType = ReturnDeviceType();

            var handler = new CreateUpdateDeviceTypeCommandHandler(
                _deviceTypeRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _mapperMock.Object,
                _loggerMock.Object);

            _mapperMock.Setup(x => x.Map<DeviceType>(command)).Returns(deviceType);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            _deviceTypeRepositoryMock.Verify(x => x.AddAsync(It.Is<DeviceType>(y => y.Id == result), default), Times.Once());
        }

        private static CreateUpdateDeviceTypeCommand ReturnCreateUpdateDeviceTypeCommand() =>
            new()
            {
                Name = "Computer",
            };

        private static DeviceType ReturnDeviceType() =>
            new()
            {
                Id = 1,
                Name = "Computer"
            };
    }
}
