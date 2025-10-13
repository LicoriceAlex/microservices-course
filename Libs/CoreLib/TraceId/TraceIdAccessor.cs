using CoreLib.TraceId.Interfaces;

namespace CoreLib.TraceId
{
    /// <summary>
    /// Класс работы с трейс айди
    /// </summary>
    internal class TraceIdAccessor : ITraceReader, ITraceWriter, ITraceIdAccessor
    {
        public string Name => TraceConstants.SerilogPropertyName;

        private string? _value;

        /// <inheritdoc />
        public string GetValue()
        {
            return _value ?? string.Empty;
        }

        /// <inheritdoc />
        public void WriteValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                value = Guid.NewGuid().ToString();
            }

            _value = value;
        }
    }

    public interface ITraceIdAccessor
    {
    }
}