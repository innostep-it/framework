using System.Globalization;

namespace InnostepIT.Framework.Core.Contract.Globalization;

public interface IGlobalizationProvider
{
    CultureInfo CultureInfo { get; }
}