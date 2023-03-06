using API.Services;
using Application.Common.Interfaces;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Persistence.Repositories;
using Application.DeviceTypes.Commands.DeleteDeviceType;
using Domain.Entities;
using FluentAssertions;
using MediatR;

namespace Application.IntegrationTests.DeviceTypes.Commands
{
    public sealed class DeleteDeviceTypeCommandHandlerTests
    {
        private readonly IDeviceTypeRepository _deviceTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteDeviceTypeCommandHandler> _logger;

        public DeleteDeviceTypeCommandHandlerTests()
        {
            var configuration = ReturnConfiguration();
            var optionsBuilder = ReturnDbOptionsBuilder(configuration);
            var currentUserService = new CurrentUserService(new HttpContextAccessor());
            var interceptor = new AuditableEntitySaveChangesInterceptor(currentUserService, new DateTimeService());
            var applicationDbContext = new ApplicationDbContext(optionsBuilder.Options, interceptor);
            var serviceProvider = ReturnServiceProvider();
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

            _deviceTypeRepository = new DeviceTypeRepository(applicationDbContext);
            _unitOfWork = new UnitOfWork(applicationDbContext);
            _logger = loggerFactory!.CreateLogger<DeleteDeviceTypeCommandHandler>();
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
        private async Task Should_DeleteDeviceType_WhenDeleteRequestIsValid()
        {
            var command = await ReturnDeleteDeviceTypeCommand();

            var handler = new DeleteDeviceTypeCommandHandler(
                _deviceTypeRepository,
                _unitOfWork,
                _logger);

            var result = await handler.Handle(command, default);

            result.Should().NotBeNull();
            result.Should().Be(Unit.Value);
        }

        private async Task<DeleteDeviceTypeCommand> ReturnDeleteDeviceTypeCommand()
        {
            var deviceType = ReturnDeviceType();

            return new() { Id = await ReturnDeviceTypeId(deviceType) };
        }

        private async Task<int> ReturnDeviceTypeId(DeviceType deviceType)
        {
            return (await _deviceTypeRepository.GetAsync(x => x.Name == deviceType.Name)).Select(x => x.Id).FirstOrDefault();
        }

        private static DeviceType ReturnDeviceType()
        {
            return new()
            {
                Name = "Computer"
            };
        }
    }
}
