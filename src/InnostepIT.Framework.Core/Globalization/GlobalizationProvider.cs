using System.Globalization;
using InnostepIT.Framework.Core.Contract.Globalization;

namespace InnostepIT.Framework.Core.Globalization
{
    public class GlobalizationProvider : IGlobalizationProvider
    {
        public CultureInfo CultureInfo { get; }

        public GlobalizationProvider(CultureInfo cultureInfo)
        {
            CultureInfo = cultureInfo;
        }
    }
}
