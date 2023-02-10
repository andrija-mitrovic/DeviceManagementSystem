using Application.Common.DTOs;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Devices.Queries.GetDeviceById
{
    internal sealed class GetDeviceByIdQueryHandler : IRequestHandler<GetDeviceByIdQuery, DeviceDetailInfoDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDeviceRepository _deviceRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDeviceByIdQueryHandler> _logger;

        public GetDeviceByIdQueryHandler(
            IApplicationDbContext context, 
            IDeviceRepository deviceRepository,
            IMapper mapper, 
            ILogger<GetDeviceByIdQueryHandler> logger)
        {
            _context = context;
            _deviceRepository = deviceRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<DeviceDetailInfoDTO> Handle(GetDeviceByIdQuery request, CancellationToken cancellationToken)
        {
            //var device = (from d in _context.Devices
            //              join dt in _context.DeviceTypes on d.DeviceTypeId equals dt.Id
            //              join dtp in _context.DeviceTypeProperties on dt.Id equals dtp.DeviceTypeId
            //              join dpv in _context.DevicePropertyValues on dtp.Id equals dpv.DeviceTypePropertyId
            //              where d.Id == request.Id
            //              select new DeviceDetailInfoDTO
            //              {
            //                  Id = d.Id,
            //                  Name = d.Name,
            //                  DeviceType = new DeviceTypeDetailInfoDTO()
            //                  {
            //                      Id = d.DeviceType!.Id,
            //                      Name = d.DeviceType.Name,
            //                      Parent = new DeviceTypeDetailInfoDTO()
            //                      {
            //                          Id = d.DeviceType.Parent!.Id,
            //                          Name = d.DeviceType.Parent!.Name,
            //                          DeviceTypeProperties = d.DeviceType.Parent!.DeviceTypeProperties!.Select(x =>
            //                              new DeviceTypePropertyDetailInfoDTO()
            //                              {
            //                                  Id = x.Id,
            //                                  Name = x.Name,
            //                                  DevicePropertyValue = d.DevicePropertyValues!.Where(y => y.DeviceTypePropertyId == x.Id).Any() ? new DevicePropertyValueDetailInfoDTO()
            //                                  {
            //                                      Id = d.DevicePropertyValues!.Where(y => y.DeviceTypePropertyId == x.Id).Select(y => y.Id).FirstOrDefault(),
            //                                      Value = d.DevicePropertyValues!.Where(y => y.DeviceTypePropertyId == x.Id).Select(y => y.Value).FirstOrDefault()
            //                                  }:null
            //                              }).ToList()
            //                      },
            //                      DeviceTypeProperties = d.DeviceType.DeviceTypeProperties!.Select(x =>
            //                             new DeviceTypePropertyDetailInfoDTO()
            //                             {
            //                                 Id = x.Id,
            //                                 Name = x.Name,
            //                                 DevicePropertyValue = new DevicePropertyValueDetailInfoDTO()
            //                                 {
            //                                     Id = d.DevicePropertyValues!.Where(y => y.DeviceTypePropertyId == x.Id).Select(y => y.Id).FirstOrDefault(),
            //                                     Value = d.DevicePropertyValues!.Where(y => y.DeviceTypePropertyId == x.Id).Select(y => y.Value).FirstOrDefault()
            //                                 }
            //                             }).ToList()
            //                  }
            //              }).FirstOrDefault();   

            //var device = await _context.Devices.Include(x => x.DeviceType)
            //                                   .ThenInclude(x => x!.Parent)
            //                                   .ThenInclude(x => x!.DeviceTypeProperties)
            //                                   .Include(x => x.DevicePropertyValues)
            //                                   .Where(x => x.Id == request.Id)
            //                                   .Select(x => new DeviceDetailInfoDTO()
            //                                   {
            //                                       Id = x.Id,
            //                                       Name = x.Name,
            //                                       DeviceType = new DeviceTypeDetailInfoDTO()
            //                                       {
            //                                           Id = x.DeviceType!.Id,
            //                                           Name = x.DeviceType.Name,
            //                                           Parent = new DeviceTypeDetailInfoDTO()
            //                                           {
            //                                               Id = x.DeviceType.Parent!.Id,
            //                                               Name = x.DeviceType.Parent.Name,
            //                                               DeviceTypeProperties = x.DeviceType.Parent.DeviceTypeProperties!.Select(y =>
            //                                                   new DeviceTypePropertyDetailInfoDTO()
            //                                                   {
            //                                                       Id = y.Id,
            //                                                       Name = y.Name,
            //                                                       DevicePropertyValue = new DevicePropertyValueDetailInfoDTO()
            //                                                       {
            //                                                           Id = x.DevicePropertyValues!.Where(z => z.DeviceTypePropertyId == y.Id).Select(z => z.Id).FirstOrDefault(),
            //                                                           Value = x.DevicePropertyValues!.Where(z => z.DeviceTypePropertyId == y.Id).Select(z => z.Value).FirstOrDefault()
            //                                                       }
            //                                                   }).ToList()
            //                                           },
            //                                           DeviceTypeProperties = x.DeviceType.DeviceTypeProperties!.Select(y =>
            //                                                new DeviceTypePropertyDetailInfoDTO()
            //                                                {
            //                                                    Id = y.Id,
            //                                                    Name = y.Name,
            //                                                    DevicePropertyValue = new DevicePropertyValueDetailInfoDTO()
            //                                                    {
            //                                                        Id = x.DevicePropertyValues!.Where(z => z.DeviceTypePropertyId == y.Id).Select(z => z.Id).FirstOrDefault(),
            //                                                        Value = x.DevicePropertyValues!.Where(z => z.DeviceTypePropertyId == y.Id).Select(z => z.Value).FirstOrDefault()
            //                                                    }
            //                                                }).ToList()
            //                                       }
            //                                   })
            //                                   .AsNoTracking()
            //                                   .FirstOrDefaultAsync(cancellationToken);

            var device = await _deviceRepository.GetDeviceDetailById(request.Id, true, cancellationToken);

            if (device is null)
            {
                _logger.LogError(nameof(DeviceType) + " with id: {DeviceTypeId} was not found.", request.Id);
                throw new DeviceNotFoundException(request.Id);
            }

            return _mapper.Map<DeviceDetailInfoDTO>(device);
        }
    }
}
