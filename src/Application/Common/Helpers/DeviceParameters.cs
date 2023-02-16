namespace Application.Common.Helpers
{
    public class DeviceParameters : RequestParameters
    {
        public DeviceParameters() => OrderBy = "name";

        public string? DeviceName { get; init; }
        public string? DeviceTypeName { get; init; }
    }
}
