using System.Security.Claims;
using InnostepIT.Framework.Core.Contract.Web;
using Microsoft.AspNetCore.Http;

namespace InnostepIT.Framework.Core.Web;

public class IdentityMiddleware
{
    private readonly IIdentityStore _identityStore;
    private readonly RequestDelegate _next;

    public IdentityMiddleware(RequestDelegate next, IIdentityStore identityStore)
    {
        _next = next;
        _identityStore = identityStore;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var requestScopeUser = (context.User.Identity as ClaimsIdentity)?.Name ?? "unknown";
        _identityStore.StoreCurrentUser(requestScopeUser);

        await _next.Invoke(context);
    }
}