using CoreLib.HttpService.Services.Models;

namespace CoreLib.HttpService.Services.Interfaces;

/// <summary>
/// Высокоуровневый сервис отправки HTTP-запросов и получения типизированных ответов
/// </summary>
public interface IHttpRequestService
{
    /// <summary>
    /// Отправить HTTP-запрос и получить типизированный результат
    /// </summary>
    Task<HttpResponse<TResponse>> SendRequestAsync<TResponse>(
        HttpRequestData requestData,
        HttpConnectionData connectionData = default);
}