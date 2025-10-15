namespace CoreLib.HttpService.Exceptions;

/// <summary>
/// Исключение транспортного уровня при выполнении HTTP-запроса
/// </summary>
public class HttpConnectionException : Exception
{
    public HttpConnectionException(string message) : base(message) { }

    public HttpConnectionException(string message, Exception inner) : base(message, inner) { }
}