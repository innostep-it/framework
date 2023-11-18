using System.Diagnostics.CodeAnalysis;
using InnostepIT.Framework.Core.FrameworkAdapter;

namespace InnostepIT.Framework.Core.Tests.FrameworkAdapter;

[TestClass]
[ExcludeFromCodeCoverage]
public class DateTimeAdapterTest
{
    private DateTimeAdapter _dateTimeAdapter;

    [TestInitialize]
    public void Initialize()
    {
        _dateTimeAdapter = new DateTimeAdapter();
    }

    [TestMethod]
    public void GetUtcDateTime_ReturnsUtc()
    {
        var expected = DateTimeKind.Utc;

        var actual = _dateTimeAdapter.GetUtcDateTime();

        Assert.AreEqual(expected, actual.Kind);
    }
}