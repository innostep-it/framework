using InnostepIT.Framework.Core.Contract.FrameworkAdapter;
using InnostepIT.Framework.Core.Contract.Web;
using InnostepIT.Framework.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace InnostepIT.Framework.Core.Tests.Data
{
    public class Person
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class FakeDbContext : CustomDbContextBase
    {
        public DbSet<Person> Persons { get; set; }

        public FakeDbContext(IIdentityStore identityStore, IDateTimeAdapter dateTimeAdapter) : base(null, dateTimeAdapter, identityStore)
        {
        }

        public FakeDbContext(IIdentityStore identityStore, IDateTimeAdapter dateTimeAdapter, DbContextOptions options) : base(null, dateTimeAdapter, identityStore)
        {
        }
    }
}
