namespace Application.Common.DTOs
{
    public sealed class DeviceTypeDetailInfoDTO
    {
        public int Id { get; init; }
        public string? Name { get; init; }
        public DeviceTypeDetailInfoDTO? Parent { get; init; } 
        public List<DeviceTypePropertyDetailInfoDTO>? DeviceTypeProperties { get; init; }
    }
}
