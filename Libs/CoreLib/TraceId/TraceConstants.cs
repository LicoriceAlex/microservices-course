namespace CoreLib.TraceId;

/// <summary>
/// Констранты для трейса
/// </summary>
public static class TraceConstants
{
    /// <summary>
    /// Заголовок трейса
    /// </summary>
    public const string HeaderName = "X-Trace-Id";
    
    /// <summary>
    /// Проперти для серилога
    /// </summary>
    public const string SerilogPropertyName = "TraceId";
}