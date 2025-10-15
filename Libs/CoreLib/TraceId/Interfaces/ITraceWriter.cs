namespace CoreLib.TraceId.Interfaces;

/// <summary>
/// Запись трассировочных значений при отправке исходящего запроса
/// </summary>
public interface ITraceWriter
{
    /// <summary>
    /// Имя
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Получить значение
    /// </summary>
    string GetValue();
}
