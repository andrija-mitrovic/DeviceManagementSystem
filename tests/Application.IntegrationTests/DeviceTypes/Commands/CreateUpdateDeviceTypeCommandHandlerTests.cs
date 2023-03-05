using API.Services;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.DeviceTypes.Commands.CreateUpdateDeviceType;
using AutoMapper;
using FluentAssertions;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Application.IntegrationTests.DeviceTypes.Commands
{
    public sealed class CreateUpdateDeviceTypeCommandHandlerTests
    {
        private readonly IDeviceTypeRepository _deviceTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateUpdateDeviceTypeCommandHandler> _logger;

        public CreateUpdateDeviceTypeCommandHandlerTests()
        {
            var configuration = ReturnConfiguration();
            var optionsBuilder = ReturnDbOptionsBuilder(configuration);
            var currentUserService = new CurrentUserService(new HttpContextAccessor());
            var interceptor = new AuditableEntitySaveChangesInterceptor(currentUserService, new DateTimeService());
            var applicationDbContext = new ApplicationDbContext(optionsBuilder.Options, interceptor);
            var mapperConfiguration = ReturnMapperConfiguration();
            var serviceProvider = ReturnServiceProvider();
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

            _deviceTypeRepository = new DeviceTypeRepository(applicationDbContext);
            _unitOfWork = new UnitOfWork(applicationDbContext);
            _mapper = mapperConfiguration.CreateMapper();
            _logger = loggerFactory!.CreateLogger<CreateUpdateDeviceTypeCommandHandler>();
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
        public async Task Handle_ShouldCreateUpdateDeviceType_WhenCreateUpdateRequestIsValid()
        {
            var command = ReturnCreateUpdateDeviceTypeCommand();

            var handler = new CreateUpdateDeviceTypeCommandHandler(
                _deviceTypeRepository,
                _unitOfWork,
                _mapper,
                _logger);

            var result = await handler.Handle(command, default);

            result.Should().BePositive();
            result.Should().NotBe(null);
        }

        private static CreateUpdateDeviceTypeCommand ReturnCreateUpdateDeviceTypeCommand() =>
            new()
            {
                Name = "Computer"
            };
    }
}
