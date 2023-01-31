using Application.Common.DTOs;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.DeviceTypes.Queries.GetDeviceTypeById
{
    internal sealed class GetDeviceTypeByIdQueryHandler : IRequestHandler<GetDeviceTypeByIdQuery, DeviceTypeDetailDTO>
    {
        private readonly IDeviceTypeRepository _deviceTypeRepository;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDeviceTypeByIdQueryHandler> _logger;

        public GetDeviceTypeByIdQueryHandler(
            IDeviceTypeRepository deviceTypeRepository,
            IApplicationDbContext context,
            IMapper mapper, 
            ILogger<GetDeviceTypeByIdQueryHandler> logger)
        {
            _deviceTypeRepository = deviceTypeRepository;
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<DeviceTypeDetailDTO> Handle(GetDeviceTypeByIdQuery request, CancellationToken cancellationToken)
        {
            //var deviceType = await _context.DeviceTypes
            //    .Include(x => x.Parent)
            //    .ThenInclude(x => x!.DeviceTypeProperties)
            //    .Include(x => x.DeviceTypeProperties)
            //    .AsNoTracking()
            //    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            var deviceType = await _deviceTypeRepository.GetDeviceTypeWithDeviceTypePropertyByIdAsync(request.Id, true, cancellationToken);

            if (deviceType is null)
            {
                _logger.LogError(nameof(DeviceType) + " with id: {DeviceTypeId} was not found.", request.Id);
                throw new DeviceTypeNotFoundException(request.Id);
            }

            return _mapper.Map<DeviceTypeDetailDTO>(deviceType);
        }
    }
}
