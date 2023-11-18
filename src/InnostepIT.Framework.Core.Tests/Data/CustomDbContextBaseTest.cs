using System.Diagnostics.CodeAnalysis;
using InnostepIT.Framework.Core.Contract.FrameworkAdapter;
using InnostepIT.Framework.Core.Contract.Web;
using Moq;

namespace InnostepIT.Framework.Core.Tests.Data;

[TestClass]
[ExcludeFromCodeCoverage]
public class CustomDbContextBaseTest
{
    private Mock<IDateTimeAdapter> _dateTimeAdapterMock;

    private FakeDbContext _fakeDbContext;
    private Mock<IIdentityStore> _identityStoreMock;

    [TestInitialize]
    public void Initialize()
    {
        _identityStoreMock = new Mock<IIdentityStore>();
        _dateTimeAdapterMock = new Mock<IDateTimeAdapter>();

        _fakeDbContext = new FakeDbContext(_identityStoreMock.Object, _dateTimeAdapterMock.Object);
    }

    [TestMethod]
    public void SaveChanges_NoChangesNoChangeTrackedEntity_WorksAsExpected()
    {
        // unit testing of dbContext doesn't work as usual, because of the way the framework is implemented
        // this needs to be tested via integration tests (postgres container running in pipeline)
    }
}