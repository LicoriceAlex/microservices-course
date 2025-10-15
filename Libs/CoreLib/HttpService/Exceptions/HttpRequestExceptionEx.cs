namespace CoreLib.HttpService.Exceptions;

/// <summary>
/// Исключение прикладного уровня при неуспешном HTTP-ответе (4xx/5xx) или ошибке десериализации
/// </summary>
public class HttpRequestExceptionEx : Exception
{
    /// <summary>HTTP-код статуса ответа</summary>
    private int StatusCode { get; }

    /// <summary>Тело ответа как строка для логов и диагностики</summary>
    private string? ResponseBody { get; }

    public HttpRequestExceptionEx(string message, int statusCode, string? responseBody = null, Exception? inner = null)
        : base(message, inner)
    {
        StatusCode = statusCode;
        ResponseBody = responseBody;
    }

    public override string ToString()
        => $"{base.ToString()}, StatusCode={StatusCode}, Body={(ResponseBody is null ? "<null>" : ResponseBody)}";
}