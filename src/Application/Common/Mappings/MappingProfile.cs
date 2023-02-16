using Application.Common.DTOs;
using Application.Devices.Commands.CreateUpdateDevice;
using Application.DeviceTypes.Commands.CreateUpdateDeviceType;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Device mapping
            CreateMap<Device, CreateUpdateDeviceCommand>().ReverseMap();
            CreateMap<Device, DeviceDetailDTO>().ReverseMap();
            CreateMap<Device, DeviceDetailInfoDTO>().ReverseMap();
            CreateMap<Device, DeviceDTO>().ReverseMap();

            // DeviceType mapping
            CreateMap<DeviceType, CreateDeviceTypeDTO>().ReverseMap();
            CreateMap<DeviceType, CreateUpdateDeviceTypeCommand>().ReverseMap();
            CreateMap<DeviceType, DeviceTypeDTO>().ReverseMap();
            CreateMap<DeviceType, DeviceTypeDetailDTO>().ReverseMap();
            CreateMap<DeviceType, DeviceTypeDetailInfoDTO>().ReverseMap();

            // DeviceTypeProperty mapping
            CreateMap<DeviceTypeProperty, CreateDeviceTypePropertyDTO>().ReverseMap();
            CreateMap<DeviceTypeProperty, DeviceTypePropertyDTO>().ReverseMap();
            CreateMap<DeviceTypeProperty, DeviceTypePropertyDetailInfoDTO>().ReverseMap();

            // DevicePropertyValue mapping
            CreateMap<DevicePropertyValue, CreateDevicePropertyValueDTO>().ReverseMap();
            CreateMap<DevicePropertyValue, DevicePropertyValueDetailInfoDTO>().ReverseMap();
        }
    }
}
