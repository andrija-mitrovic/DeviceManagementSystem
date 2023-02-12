namespace Application.Common.DTOs
{
    public sealed class DeviceDetailDTO
    {
        public int Id { get; init; }
        public string? Name { get; init; }
        public DeviceTypeDetailDTO? DeviceType { get; init; }
    }
}
