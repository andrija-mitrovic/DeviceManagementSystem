namespace Application.Common.DTOs
{
    public sealed class CreateDevicePropertyValueDTO
    {
        public int Id { get; set; }
        public string? Value { get; set; }
        public int DeviceTypePropertyId { get; set; }
    }
}
