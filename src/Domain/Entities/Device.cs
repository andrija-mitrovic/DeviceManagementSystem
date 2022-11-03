using Domain.Common;

namespace Domain.Entities
{
    public sealed class Device : BaseEntity
    {
        public string? Name { get; set; }
        public DeviceType? DeviceType { get; set; }
        public int DeviceTypeId { get; set; }
        public List<DevicePropertyValue>? DevicePropertyValues { get; set; }
    }
}
