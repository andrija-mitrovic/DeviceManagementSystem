namespace Application.Common.DTOs
{
    public sealed class DeviceTypePropertyDetailInfoDTO
    {
        public int Id { get; init; }
        public string? Name { get; init; }
        public DevicePropertyValueDetailInfoDTO? DevicePropertyValue { get; init; }
    }
}
