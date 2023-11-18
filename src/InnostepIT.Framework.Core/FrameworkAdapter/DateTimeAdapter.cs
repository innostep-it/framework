using InnostepIT.Framework.Core.Contract.FrameworkAdapter;

namespace InnostepIT.Framework.Core.FrameworkAdapter;

public class DateTimeAdapter : IDateTimeAdapter
{
    public DateTime GetUtcDateTime()
    {
        return DateTime.UtcNow;
    }

    public DateTime GetRegionalDateTime()
    {
        return DateTime.Now;
    }
}