using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Devices.Commands.CreateDevice
{
    internal sealed class CreateDeviceCommandHandler : IRequestHandler<CreateDeviceCommand, int>
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IDeviceTypeRepository _deviceTypeRepository;
        private readonly IDeviceTypePropertyRepository _deviceTypePropertyRepository;
        private readonly IDevicePropertyValueRepository _devicePropertyValueRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateDeviceCommandHandler> _logger;

        public CreateDeviceCommandHandler(
            IDeviceRepository deviceRepository,
            IDeviceTypeRepository deviceTypeRepository,
            IDeviceTypePropertyRepository deviceTypePropertyRepository,
            IDevicePropertyValueRepository devicePropertyValueRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<CreateDeviceCommandHandler> logger)
        {
            _deviceRepository = deviceRepository;
            _deviceTypeRepository = deviceTypeRepository;
            _deviceTypePropertyRepository = deviceTypePropertyRepository;
            _devicePropertyValueRepository = devicePropertyValueRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> Handle(CreateDeviceCommand request, CancellationToken cancellationToken)
        {
            var device = await _deviceRepository.GetByIdAsync(request.Id, cancellationToken);

            if (device == null)
            {
                _logger.LogInformation($"Creating {nameof(Device)}...");

                device = _mapper.Map<Device>(request);

                await _deviceRepository.AddAsync(device, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                var devicePropertyValues = await _devicePropertyValueRepository.GetDevicePropertyValuesByDeviceId(device.Id);
                var parentDevicePropertyValue = await _devicePropertyValueRepository.GetParentDevicePropertyValuesByDeviceId(device.Id);

                devicePropertyValues.ForEach(x => x.DeviceId = device.Id);
                parentDevicePropertyValue.ForEach(x => x.DeviceId = device.Id);

                _logger.LogInformation(nameof(Device) + " with Id: {DeviceId} is successfully created.", device.Id);
            }
            else
            {
                _logger.LogInformation($"Updating {nameof(Device)}...");

                var deviceType = await _deviceTypeRepository.GetByIdAsync(device.DeviceTypeId);
                var parentId = deviceType!.ParentId.GetValueOrDefault();
                var parentDeviceTypes = await _deviceTypeRepository.GetDeviceTypeWithPropertiesAndValuesById(parentId);

                device.Name = request.Name;

                if (deviceType != null)
                {
                    //deviceType.Name = request.DeviceType!.Name;
                }

                //var deviceType = await _deviceTypeRepository.GetByIdAsync(device.DeviceTypeId);

                //if (deviceType != null)
                //{
                //    var devicePropertyValues = await _devicePropertyValueRepository.GetDevicePropertyValuesByDeviceId(device.Id);
                //    var parentDevicePropertyValue = await _devicePropertyValueRepository.GetParentDevicePropertyValuesByDeviceId(device.Id);

                //    devicePropertyValues.ForEach(x => x.DeviceId = null);
                //    parentDevicePropertyValue.ForEach(x => x.DeviceId = null);

                //    var parentId = deviceType.ParentId.GetValueOrDefault();

                //    _deviceTypeRepository.Delete(deviceType);

                //    await RemoveParentDevice(parentId);

                //    await _unitOfWork.SaveChangesAsync(cancellationToken);
                //}

                //_mapper.Map(request, device);
                //_deviceRepository.Update(device);

                //_logger.LogInformation(nameof(Device) + " with Id: {DeviceId} is successfully updated.", device.Id);

                //var parent = await _deviceTypeRepository.GetParentDeviceTypeId(device.DeviceTypeId);
                //var deviceType = await _deviceTypeRepository.GetDeviceTypeWitParentById(device.DeviceTypeId);
                //var parentDeviceType = await _deviceTypeRepository.GetParentDeviceTypeById(deviceType?.ParentId);

                ////How to delete self referencing table data using entityframework
                //if (deviceType != null) _deviceTypeRepository.Delete(deviceType!);
                //if (parentDeviceType != null) _deviceTypeRepository.Delete(parentDeviceType);

                //device.DeviceType = _mapper.Map<DeviceType>(request.DeviceType);
                //device.DeviceType!.DeviceTypeProperties!.ForEach(x => x.DevicePropertyValue!.DeviceId = device.Id);
                //device.DeviceType!.Parent!.DeviceTypeProperties!.ForEach(x => x.DevicePropertyValue!.DeviceId = device.Id);

                //if (device.DeviceType != null)
                //{
                //    _deviceRepository.Update(device);
                //}

                //_logger.LogInformation(nameof(Device) + " with Id: {DeviceId} is successfully updated.", device.Id);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return device.Id;
        }

        private async Task RemoveParentDevice(int parentId)
        {
            var numberOfDeviceType = await _deviceTypeRepository.GetNumberOfDeviceTypeByParentId(parentId);

            if (numberOfDeviceType == 1)
            {
                var parentDeviceType = await _deviceTypeRepository.GetParentDeviceTypeById(parentId);
                var parentDeviceTypeParentId = parentDeviceType?.ParentId;

                _deviceTypeRepository.Delete(parentDeviceType!);

                if (parentDeviceTypeParentId != null)
                {
                    await RemoveParentDevice(parentDeviceTypeParentId.GetValueOrDefault());
                }
            }

            return;
        }

        //public async Task<int> Handle(CreateDeviceCommand request, CancellationToken cancellationToken)
        //{
        //    var device = await _deviceRepository.GetByIdAsync(request.Id);

        //    if (device == null)
        //    {
        //        device = _mapper.Map<Device>(request);

        //        await _deviceRepository.AddAsync(device, cancellationToken);

        //        await _unitOfWork.SaveChangesAsync(cancellationToken);

        //        var devicePropertyValues = await _devicePropertyValueRepository.GetDevicePropertyValuesByDeviceId(device.Id);
        //        var parentDevicePropertyValue = await _devicePropertyValueRepository.GetParentDevicePropertyValuesByDeviceId(device.Id);

        //        devicePropertyValues.ForEach(x => x.DeviceId = device.Id);
        //        parentDevicePropertyValue.ForEach(x => x.DeviceId = device.Id);

        //        _logger.LogInformation(nameof(Device) + " with Id: {DeviceId} is successfully created.", device.Id);
        //    }
        //    else
        //    {

        //        var deviceType = await _deviceTypeRepository.GetDeviceTypeWitParentById(device.DeviceTypeId);
        //        var parentDeviceType = await _deviceTypeRepository.GetParentDeviceTypeById(deviceType?.ParentId);

        //        //How to delete self referencing table data using entityframework
        //        if (deviceType != null) _deviceTypeRepository.Delete(deviceType!);
        //        if (parentDeviceType != null) _deviceTypeRepository.Delete(parentDeviceType);

        //        device.DeviceType = _mapper.Map<DeviceType>(request.DeviceType);
        //        device.DeviceType!.DeviceTypeProperties!.ForEach(x => x.DevicePropertyValue!.DeviceId = device.Id);
        //        device.DeviceType!.Parent!.DeviceTypeProperties!.ForEach(x => x.DevicePropertyValue!.DeviceId = device.Id);

        //        if (device.DeviceType != null) 
        //        { 
        //            _deviceRepository.Update(device); 
        //        }

        //        _logger.LogInformation(nameof(Device) + " with Id: {DeviceId} is successfully updated.", device.Id);
        //    }

        //    await _unitOfWork.SaveChangesAsync(cancellationToken);

        //    return device.Id;
        //}

        //private async Task RemoveDeviceType(int index)
        //{
        //    var deviceType = await _deviceTypeRepository.GetDeviceTypeWitParentById(device.DeviceTypeId);
        //}

        //public async Task<int> Handle(CreateDeviceCommand request, CancellationToken cancellationToken)
        //{
        //    var device = await _deviceRepository.GetByIdAsync(request.Id);

        //    if (device == null)
        //    {
        //        device = _mapper.Map<Device>(request);

        //        await _deviceRepository.AddAsync(device, cancellationToken);

        //        _logger.LogInformation(nameof(Device) + " with Id: {DeviceId} is successfully created.", device.Id);
        //    }
        //    else
        //    {
        //        var deviceType = await _deviceTypeRepository.GetByIdAsync(request.DeviceType!.Id);

        //        if (deviceType == null)
        //        {
        //            _logger.LogError(nameof(DeviceType) + " with Id: {DeviceTypeId} was not found.", request?.DeviceType?.Id);
        //            throw new NotFoundException(nameof(DeviceType), request?.DeviceType.Id!);
        //        }

        //        var deviceTypeParent = await _deviceTypeRepository.GetParentDeviceTypeById(request.DeviceType.Parent!.Id);

        //        if (deviceTypeParent == null)
        //        {
        //            _logger.LogError(nameof(DeviceType) + " with ParentId: {DeviceTypeParentId} was not found.", request?.DeviceType?.Parent.Id);
        //            throw new NotFoundException(nameof(DeviceType), request?.DeviceType.Parent.Id!);
        //        }

        //        _mapper.Map(request!.DeviceType, deviceType);

        //        foreach (var deviceTypePropertyDto in request.DeviceType.DeviceTypeProperties!) 
        //        {
        //            var deviceTypeProperty = await _deviceTypePropertyRepository.GetByIdAsync(deviceTypePropertyDto.Id);

        //            _mapper.Map(deviceTypePropertyDto, deviceTypeProperty);
        //        }

        //        _deviceTypeRepository.Update(deviceType);

        //        _logger.LogInformation(nameof(Device) + " with Id: {DeviceId} is successfully updated.", device.Id);
        //    }

        //    await _unitOfWork.SaveChangesAsync(cancellationToken);

        //    return device.Id;
        //}
    }
}
