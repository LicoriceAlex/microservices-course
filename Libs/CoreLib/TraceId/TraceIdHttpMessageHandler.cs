using CoreLib.TraceId.Interfaces;

namespace CoreLib.TraceId;

/// <summary>
/// добавляет X-Trace-Id в исходящие запросы
/// </summary>
public class TraceIdHttpMessageHandler : DelegatingHandler
{
    private readonly ITraceWriter _traceWriter;

    public TraceIdHttpMessageHandler(ITraceWriter traceWriter)
    {
        _traceWriter = traceWriter;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var current = _traceWriter.GetValue();
        if (!string.IsNullOrWhiteSpace(current))
        {
            if (!request.Headers.Contains(TraceConstants.HeaderName))
            {
                request.Headers.Add(TraceConstants.HeaderName, current);
            }
        }

        return await base.SendAsync(request, cancellationToken);
    }
}