using System.Text;
using System.Text.Json;
using InnostepIT.Framework.Core.Contract.FrameworkAdapter;
using Microsoft.Extensions.Logging;

namespace InnostepIT.Framework.Core.FrameworkAdapter;

public class HttpClientAdapterFake : IHttpClientAdapter
{
    private readonly ILogger<HttpClientAdapterFake> _logger;

    public HttpClientAdapterFake(ILogger<HttpClientAdapterFake> logger)
    {
        _logger = logger;
    }

    public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
    {
        _logger.LogInformation("SenAsync has been called with request: {Request}", request);

        var requestJson = JsonSerializer.Serialize(request.Content);
        var uniqueFileName = Guid.NewGuid().ToString();
        await File.WriteAllTextAsync("./test-output/http-sendasync-" + uniqueFileName + ".json", requestJson,
            Encoding.UTF8);

        return await Task.FromResult(new HttpResponseMessage());
    }

    public async Task<HttpResponseMessage> PostAsync(string? requestUri, HttpContent? content)
    {
        _logger.LogInformation("PostAsync has been called with requestUri: {RequestUri} and content: {Content}",
            requestUri, content);

        var requestJson = JsonSerializer.Serialize(content);
        var uniqueFileName = Guid.NewGuid().ToString();
        await File.WriteAllTextAsync("./test-output/http-postasync-" + uniqueFileName + ".json", requestJson,
            Encoding.UTF8);

        return await Task.FromResult(new HttpResponseMessage());
    }
}