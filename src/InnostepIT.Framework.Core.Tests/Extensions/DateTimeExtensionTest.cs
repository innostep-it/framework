using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using InnostepIT.Framework.Core.Extensions;

namespace InnostepIT.Framework.Core.Tests.Extensions
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DateTimeExtensionTest
    {
        [TestMethod]
        public void GetQuarter_DateIsBeginningOfYear_Returns1()
        {
            // Arrange
            var input = new DateTime(2021, 3, 1);
            var expected = 1;

            // Act
            var result = input.GetQuarter();

            // Assert
            result.Should().Be(expected);
        }

        [TestMethod]
        public void GetQuarter_DateIsEndOfYear_Returns4()
        {
            // Arrange
            var input = new DateTime(2021, 12, 1);
            var expected = 4;

            // Act
            var result = input.GetQuarter();

            // Assert
            result.Should().Be(expected);
        }

        [TestMethod]
        public void GetQuartersBetweenDates_SecondDate2YearsAgo_ReturnsQuarters()
        {
            // Arrange
            var firstDate = new DateTime(2021, 12, 1);
            var secondDate = new DateTime(2019, 10, 8);

            const int expectedAmountOfQuartersBetween = 8;

            // Act
            var result = firstDate.GetQuartersBetweenDates(secondDate);

            // Assert
            result.Count().Should().Be(expectedAmountOfQuartersBetween);
        }
    }
}
