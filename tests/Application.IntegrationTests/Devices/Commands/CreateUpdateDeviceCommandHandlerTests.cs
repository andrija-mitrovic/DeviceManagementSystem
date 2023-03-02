using API.Services;
using Application.Common.Interfaces;
using Application.Devices.Commands.CreateUpdateDevice;
using AutoMapper;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Application.Common.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Persistence.Repositories;
using FluentAssertions;

namespace Application.IntegrationTests.Devices.Commands
{
    public sealed class CreateUpdateDeviceCommandHandlerTests
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateUpdateDeviceCommandHandler> _logger;

        public CreateUpdateDeviceCommandHandlerTests()
        {
            var configuration = ReturnConfiguration();
            var optionsBuilder = ReturnDbOptionsBuilder(configuration);
            var currentUserService = new CurrentUserService(new HttpContextAccessor());
            var interceptor = new AuditableEntitySaveChangesInterceptor(currentUserService, new DateTimeService());
            var applicationDbContext = new ApplicationDbContext(optionsBuilder.Options, interceptor);
            var mapperConfiguration = ReturnMapperConfiguration();
            var serviceProvider = ReturnServiceProvider();
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

            _deviceRepository = new DeviceRepository(applicationDbContext);
            _unitOfWork = new UnitOfWork(applicationDbContext);
            _mapper = mapperConfiguration.CreateMapper();
            _logger = loggerFactory!.CreateLogger<CreateUpdateDeviceCommandHandler>();
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

        private static MapperConfiguration ReturnMapperConfiguration()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
        }

        private static ServiceProvider ReturnServiceProvider()
        {
            return new ServiceCollection().AddLogging().BuildServiceProvider();
        }

        [Fact]
        public async Task Handle_ShouldCreateUpdateDevice_WhenCreateUpdateRequestIsValid()
        {
            var command = ReturnCreateUpdateDeviceCommand();

            var handler = new CreateUpdateDeviceCommandHandler(
                _deviceRepository,
                _unitOfWork,
                _mapper,
                _logger);

            var result = await handler.Handle(command, default);

            result.Should().BePositive();
            result.Should().NotBe(null);
        }

        private static CreateUpdateDeviceCommand ReturnCreateUpdateDeviceCommand() =>
            new()
            {
                Name = "Samsung Galaxy",
                DeviceTypeId = 1
            };
    }
}
