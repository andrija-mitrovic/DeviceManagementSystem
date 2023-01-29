using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence
{
    public sealed class ApplicationDbContextInitialiser
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ApplicationDbContextInitialiser> _logger;

        public ApplicationDbContextInitialiser(
            ApplicationDbContext context,
            ILogger<ApplicationDbContextInitialiser> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer())
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            if (_context.Devices.Any()) return;

            List<DeviceType> deviceTypes = GetDeviceTypes();

            await _context.AddRangeAsync(deviceTypes);
            await _context.SaveChangesAsync();
        }

        private static List<DeviceType> GetDeviceTypes()
        {
            var deviceType = new DeviceType
            {
                Name = "Racunar",
                DeviceTypeProperties = new List<DeviceTypeProperty>()
                    {
                        new DeviceTypeProperty() { Name = "RAM memorija" },
                        new DeviceTypeProperty() { Name = "Operativni sistem" },
                        new DeviceTypeProperty() { Name = "Processor" }
                    }
            };

            return new List<DeviceType>
            {
                deviceType,
                new DeviceType
                {
                    Name = "Laptop",
                    Parent = deviceType,
                    DeviceTypeProperties = new List<DeviceTypeProperty>()
                    {
                        new DeviceTypeProperty() { Name = "Dijagonala ekrana" }
                    }
                }
            };
        }
    }
}
