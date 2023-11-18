using System.Globalization;
using InnostepIT.Framework.Core.Contract.Globalization;

namespace InnostepIT.Framework.Core.Globalization;

public class GlobalizationProvider : IGlobalizationProvider
{
    public GlobalizationProvider(CultureInfo cultureInfo)
    {
        CultureInfo = cultureInfo;
    }

    public CultureInfo CultureInfo { get; }
}