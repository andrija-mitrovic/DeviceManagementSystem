namespace Application.Common.DTOs
{
    public sealed class CreateDeviceTypeDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public CreateDeviceTypeDTO? Parent { get; set; }
        public List<CreateDeviceTypePropertyDTO>? DeviceTypeProperties { get; set; }
    }
}
