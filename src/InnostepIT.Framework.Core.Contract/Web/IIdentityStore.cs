namespace InnostepIT.Framework.Core.Contract.Web;

public interface IIdentityStore
{
    void StoreCurrentUser(string username);
    string GetCurrentUser();
}