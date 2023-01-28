using Application.Common.DTOs;
using Application.Devices.Commands.CreateDevice;
using Application.DeviceTypes.Commands.CreateDeviceType;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Device mapping
            CreateMap<Device, CreateDeviceCommand>().ReverseMap();

            // DeviceType mapping
            CreateMap<DeviceType, CreateDeviceTypeDTO>().ReverseMap();
            CreateMap<DeviceType, CreateDeviceTypeCommand>().ReverseMap();
            CreateMap<DeviceType, DeviceTypeDTO>().ReverseMap();
            CreateMap<DeviceType, DeviceTypeDetailDTO>().ReverseMap();

            // DeviceTypeProperty mapping
            CreateMap<DeviceTypeProperty, CreateDeviceTypePropertyDTO>().ReverseMap();
            CreateMap<DeviceTypeProperty, DeviceTypePropertyDTO>().ReverseMap();

            // DevicePropertyValue mapping
            CreateMap<DevicePropertyValue, CreateDevicePropertyValueDTO>().ReverseMap();
        }
    }
}
