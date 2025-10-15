using CoreLib.HttpService.Exceptions;
using CoreLib.HttpService.Services.Interfaces;
using CoreLib.HttpService.Services.Models;

namespace CoreLib.HttpService.Services;

/// <summary>
/// Реализация соединения: создаёт HttpClient и отправляет запросы
/// </summary>
internal sealed class HttpConnectionService : IHttpConnectionService
{
    // Фабрика клиентов позволяет переиспользовать сокеты
    private readonly IHttpClientFactory _httpClientFactory;

    public HttpConnectionService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    /// <inheritdoc />
    public HttpClient CreateHttpClient(HttpConnectionData data)
    {
        var httpClient = string.IsNullOrWhiteSpace(data.ClientName)
            ? _httpClientFactory.CreateClient()
            : _httpClientFactory.CreateClient(data.ClientName);

        // Переопределяем таймаут при необходимости
        if (data.Timeout is not null)
            httpClient.Timeout = data.Timeout.Value;

        return httpClient;
    }

    /// <inheritdoc />
    public async Task<HttpResponseMessage> SendRequestAsync(
        HttpRequestMessage httpRequestMessage,
        HttpClient httpClient,
        CancellationToken cancellationToken,
        HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead)
    {
        try
        {
            // Отправляем запрос; HttpCompletionOption управляет тем, когда задача завершается
            return await httpClient.SendAsync(httpRequestMessage, httpCompletionOption, cancellationToken);
        }
        catch (TaskCanceledException ex) when (!cancellationToken.IsCancellationRequested)
        {
            // Истёк таймаут клиента
            throw new HttpConnectionException("HTTP request timed out", ex);
        }
        catch (Exception ex)
        {
            // Прочие сетевые ошибки
            throw new HttpConnectionException("HTTP transport error", ex);
        }
    }
}
