using InnostepIT.Framework.Core.Contract.FrameworkAdapter;

namespace InnostepIT.Framework.Core.FrameworkAdapter;

public class HttpClientAdapter : IHttpClientAdapter
{
    private readonly HttpClient _httpClient;

    public HttpClientAdapter()
    {
        _httpClient = new HttpClient();
    }
    
    public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
    {
        return await _httpClient.SendAsync(request);
    }

    public async Task<HttpResponseMessage> PostAsync(string? requestUri, HttpContent? content)
    {
        return await _httpClient.PostAsync(requestUri, content);
    }
}