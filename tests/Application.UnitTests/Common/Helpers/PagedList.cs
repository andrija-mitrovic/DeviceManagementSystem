using Domain.Entities;
using FluentAssertions;

namespace Application.UnitTests.Common.Helpers
{
    public sealed class PagedList
    {
        [Fact]
        public void DefaultConstructorCreatesPagedListInstance()
        {
            var devices = ReturnDevices();

            var result = new Application.Common.Helpers.PagedList<Device>(devices, 4, 1, 2);

            result.MetaData.CurrentPage.Should().Be(1);
            result.MetaData.TotalPages.Should().Be(2);
            result.MetaData.TotalCount.Should().Be(4);
        }

        private static List<Device> ReturnDevices()
        {
            return new List<Device>
            {
                new Device() { Name = "Dell Inspirion 15000" },
                new Device() { Name = "HP 5400" },
                new Device() { Name = "IPhone X" },
                new Device() { Name = "Samsung Galaxy" }
            };
        }
    }
}
