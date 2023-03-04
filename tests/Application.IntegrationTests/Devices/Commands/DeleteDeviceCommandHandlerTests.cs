using API.Services;
using Application.Common.Interfaces;
using Application.Devices.Commands.DeleteDevice;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Persistence.Repositories;
using Domain.Entities;
using FluentAssertions;
using MediatR;

namespace Application.IntegrationTests.Devices.Commands
{
    public sealed class DeleteDeviceCommandHandlerTests
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IDevicePropertyValueRepository _devicePropertyValueRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteDeviceCommandHandler> _logger;

        public DeleteDeviceCommandHandlerTests()
        {
            var configuration = ReturnConfiguration();
            var optionsBuilder = ReturnDbOptionsBuilder(configuration);
            var currentUserService = new CurrentUserService(new HttpContextAccessor());
            var interceptor = new AuditableEntitySaveChangesInterceptor(currentUserService, new DateTimeService());
            var applicationDbContext = new ApplicationDbContext(optionsBuilder.Options, interceptor);
            var serviceProvider = ReturnServiceProvider();
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

            _deviceRepository = new DeviceRepository(applicationDbContext);
            _devicePropertyValueRepository = new DevicePropertyValueRepository(applicationDbContext);
            _unitOfWork = new UnitOfWork(applicationDbContext);
            _logger = loggerFactory!.CreateLogger<DeleteDeviceCommandHandler>();
        }

        private static IConfigurationRoot ReturnConfiguration()
        {
            return new ConfigurationBuilder().AddJsonFile(Constants.APPSETTINGS_FILE, true, true).Build();
        }

        private static DbContextOptionsBuilder<ApplicationDbContext> ReturnDbOptionsBuilder(IConfigurationRoot configuration)
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(configuration.GetConnectionString(Constants.CONNECTION_STRING_NAME));
        }

        private static ServiceProvider ReturnServiceProvider()
        {
            return new ServiceCollection().AddLogging().BuildServiceProvider();
        }

        [Fact]
        public async Task Should_DeleteDevice_WhenDeleteRequestIsValid()
        {
            var command = await ReturnDeleteDeviceCommand();

            var handler = new DeleteDeviceCommandHandler(
                _deviceRepository,
                _devicePropertyValueRepository,
                _unitOfWork,
                _logger);

            var result = await handler.Handle(command, default);

            result.Should().NotBeNull();
            result.Should().Be(Unit.Value);
        }

        private async Task<DeleteDeviceCommand> ReturnDeleteDeviceCommand()
        {
            var device = ReturnDevice();

            return new() { Id = await ReturnDeviceId(device) };
        }

        private async Task<int> ReturnDeviceId(Device device)
        {
            return (await _deviceRepository.GetAsync(x => x.Name == device.Name)).Select(x => x.Id).FirstOrDefault();
        }

        private static Device ReturnDevice()
        {
            return new()
            {
                Name = "Samsung Galaxy"
            };
        }
    }
}
