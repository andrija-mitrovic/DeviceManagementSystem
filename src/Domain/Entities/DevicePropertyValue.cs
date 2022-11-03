using Domain.Common;

namespace Domain.Entities
{
    public sealed class DevicePropertyValue : BaseEntity
    {
        public DeviceTypeProperty? DeviceTypeProperty { get; set; }
        public int DeviceTypePropertyId { get; set; }
        public Device? Device { get; set; }
        public int DeviceId { get; set; }
        public string? Value { get; set; }
    }
}
