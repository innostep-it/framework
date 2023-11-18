namespace InnostepIT.Framework.Core.Contract.FrameworkAdapter;

public interface IHttpClientAdapter
{
    Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    Task<HttpResponseMessage>  PostAsync(string? requestUri, HttpContent? content);
}