using FluentAssertions;
using Xunit;

namespace Bonbonniere.UnitTests
{
    public class SampleTest
    {
        [Fact(Skip = "Test")]
        public void FirstTest()
        {
            var a = 1;
            var b = 1;
            a.Should().Equals(b);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 4)]
        public void SecondTest(int firstNumber, int secondNumber)
        {
            firstNumber.Should().Be(secondNumber);
        }
    }
}
