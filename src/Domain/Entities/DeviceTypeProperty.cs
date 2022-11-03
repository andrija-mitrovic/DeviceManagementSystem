using Domain.Common;

namespace Domain.Entities
{
    public sealed class DeviceTypeProperty : BaseEntity
    {
        public string? Name { get; set; }
        public DeviceType? DeviceType { get; set; }
        public int DeviceTypeId { get; set; }
        public DevicePropertyValue? DevicePropertyValue { get; set; }
    }
}
