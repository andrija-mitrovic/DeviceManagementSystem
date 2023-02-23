using Application.Common.Helpers;
using FluentAssertions;

namespace Application.UnitTests.Common.Helpers
{
    public sealed class HelperFunctionTest
    {
        [Theory]
        [InlineData("DeviceCreate")]
        public void GetMethodName_ShouldGetMethodName_WhenArgumentIsPassed(string input)
        {
            var result = HelperFunction.GetMethodName(input);

            result.Should().BeEquivalentTo(input);
        }
    }
}
