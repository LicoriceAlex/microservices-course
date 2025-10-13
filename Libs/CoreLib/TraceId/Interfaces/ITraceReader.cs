namespace CoreLib.TraceId.Interfaces;

/// <summary>
/// Чтение трассировочных значений при создании нового scoped
/// Для HTTP — это делает middleware
/// </summary>
public interface ITraceReader
{
    /// <summary>
    /// Имя
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Записать значение
    /// </summary>
    void WriteValue(string value);
}