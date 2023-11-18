using InnostepIT.Framework.Core.Contract.Web;

namespace InnostepIT.Framework.Core;

public class IdentityStore : IIdentityStore
{
    private string _username = "undefined";

    public void StoreCurrentUser(string username)
    {
        _username = username;
    }

    public string GetCurrentUser()
    {
        return _username;
    }
}