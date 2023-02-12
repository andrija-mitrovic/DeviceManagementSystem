namespace Application.Common.DTOs
{
    public sealed class DeviceDetailInfoDTO
    {
        public int Id { get; init; }
        public string? Name { get; init; }
        public DeviceTypeDetailInfoDTO? DeviceType { get; init; }
    }
}
