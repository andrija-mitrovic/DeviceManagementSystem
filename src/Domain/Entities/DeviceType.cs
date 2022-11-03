using Domain.Common;

namespace Domain.Entities
{
    public sealed class DeviceType : BaseEntity
    {
        public string? Name { get; set; }
        public DeviceType? Parent { get; set; }
        public int? ParentId { get; set; }
        public List<DeviceType>? Children { get; set; }
        public List<Device>? Devices { get; set; }
        public List<DeviceTypeProperty>? DeviceTypeProperties { get; set; }
    }
}
