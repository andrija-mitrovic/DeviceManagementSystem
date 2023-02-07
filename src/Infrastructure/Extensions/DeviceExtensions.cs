using Application.Common.Helpers;
using Domain.Entities;

namespace System.Linq
{
    internal static class DeviceExtensions
    {
        public static IQueryable<Device> SearchDeviceByParameters(this IQueryable<Device> devices, DeviceParameters? deviceParameters)
        {
            if (!string.IsNullOrWhiteSpace(deviceParameters?.DeviceName))
            {
                devices = devices.Where(x => x.Name!.Contains(deviceParameters.DeviceName, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(deviceParameters?.DeviceTypeName))
            {
                devices = devices.Where(x => x.DeviceType!.Name!.Contains(deviceParameters.DeviceName!, StringComparison.OrdinalIgnoreCase));
            }

            return devices;
        }
    }
}
