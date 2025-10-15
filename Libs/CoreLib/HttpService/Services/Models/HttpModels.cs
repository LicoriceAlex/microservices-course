using System.Net;
using System.Net.Http.Headers;

namespace CoreLib.HttpService.Services.Models;

/// <summary>
/// Перечень поддерживаемых Content-Type для тела запроса
/// </summary>
public enum ContentType
{
    Unknown = 0,
    ApplicationJson = 1,
    XWwwFormUrlEncoded = 2,
    Binary = 3,
    ApplicationXml = 4,
    MultipartFormData = 5,
    TextXml = 6,
    TextPlain = 7,
    ApplicationJwt = 8
}

/// <summary>
/// Параметры HTTP-подключения и выполнения запроса
/// </summary>
public readonly record struct HttpConnectionData()
{
    /// <summary>
    /// Таймаут HttpClient
    /// </summary>
    public TimeSpan? Timeout { get; init; }

    /// <summary>
    /// Токен отмены для конкретного вызова
    /// </summary>
    public CancellationToken CancellationToken { get; init; }

    /// <summary>
    /// Имя именованного клиента фабрики
    /// </summary>
    public string? ClientName { get; init; }

    /// <summary>
    /// Опция завершения ответа для SendAsync
    /// </summary>
    public HttpCompletionOption CompletionOption { get; init; } = HttpCompletionOption.ResponseContentRead;
}

/// <summary>
/// Данные исходящего HTTP-запроса
/// </summary>
public record HttpRequestData
{
    /// <summary>
    /// HTTP-метод
    /// </summary>
    public HttpMethod Method { get; set; } = HttpMethod.Get;

    /// <summary>
    /// Адрес запроса
    /// </summary>
    public Uri? Uri { get; set; }

    /// <summary>
    /// Тело запроса
    /// </summary>
    public object? Body { get; set; }

    /// <summary>
    /// Тип содержимого для тела запроса
    /// </summary>
    public ContentType ContentType { get; set; } = ContentType.ApplicationJson;

    /// <summary>
    /// Произвольные заголовки запроса
    /// </summary>
    public IDictionary<string, string> HeaderDictionary { get; set; } =
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Набор query-параметров, которые будут добавлены к Uri
    /// </summary>
    public ICollection<KeyValuePair<string, string>> QueryParameterList { get; set; } =
        new List<KeyValuePair<string, string>>();
}

/// <summary>
/// Базовая часть HTTP-ответа
/// </summary>
public record BaseHttpResponse
{
    /// <summary>
    /// Код статуса
    /// </summary>
    public HttpStatusCode StatusCode { get; init; }

    /// <summary>
    /// Заголовки ответа
    /// </summary>
    public HttpResponseHeaders Headers { get; init; } = null!;

    /// <summary>
    /// Заголовки
    /// </summary>
    public HttpContentHeaders? ContentHeaders { get; init; }

    /// <summary>
    /// Признак успешного ответа (2xx)
    /// </summary>
    public bool IsSuccessStatusCode => (int)StatusCode is >= 200 and <= 299;
}

/// <summary>
/// Типизированный HTTP-ответ
/// </summary>
public sealed record HttpResponse<TResponse> : BaseHttpResponse
{
    /// <summary>
    /// Тело ответа после десериализации
    /// </summary>
    public TResponse? Body { get; init; }
}
