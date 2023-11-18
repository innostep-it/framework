namespace InnostepIT.Framework.Core.Contract.Configuration
{
    public interface IConfigurator
    {
        void Set(string category, string key, object value, bool persist = true);
        TItem Get<TItem>(string category, string key);
    }
}
