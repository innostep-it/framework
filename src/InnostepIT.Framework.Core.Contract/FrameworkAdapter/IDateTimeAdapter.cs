namespace InnostepIT.Framework.Core.Contract.FrameworkAdapter;

public interface IDateTimeAdapter
{
    DateTime GetUtcDateTime();
    DateTime GetRegionalDateTime();
}