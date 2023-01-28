namespace Application.Common.DTOs
{
    public sealed class DeviceTypeDetailDTO
    {
        public int Id { get; init; }
        public string? Name { get; init; }
        public DeviceTypeDetailDTO? Parent { get; init; }
        public List<DeviceTypePropertyDTO>? DeviceTypeProperties { get; init; }
    }
}
