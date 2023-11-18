using InnostepIT.Framework.Core.Contract.Configuration;
using InnostepIT.Framework.Core.Contract.Configuration.Exceptions;

namespace InnostepIT.Framework.Core.Configuration;

public class InMemoryConfigurator : IConfigurator
{
    private readonly Dictionary<string, object> _items;

    public InMemoryConfigurator()
    {
        _items = new Dictionary<string, object>();
    }

    public void Set(string category, string key, object value, bool persist = true)
    {
        var combinedKey = GetCombinedKey(category, key);
        _items[combinedKey] = value;
    }

    public TItem Get<TItem>(string category, string key)
    {
        var combinedKey = GetCombinedKey(category, key);

        object item;
        var containsMatchingElement = _items.TryGetValue(combinedKey, out item);

        if (!containsMatchingElement)
            throw new ConfigurationNotFoundException(
                $"No configuration entry with category='{category}' and key='{key}' found.");

        return (TItem)item;
    }

    private string GetCombinedKey(string category, string key)
    {
        return category + "." + key;
    }
}