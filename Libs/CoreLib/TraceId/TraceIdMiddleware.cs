using CoreLib.TraceId.Interfaces;
using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace CoreLib.TraceId
{
    /// <summary>
    /// Читает/создаёт TraceId, кладёт его в LogContext и в ответ
    /// </summary>
    public class TraceIdMiddleware
    {
        private readonly RequestDelegate _next;

        public TraceIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ITraceReader traceReader, ITraceWriter traceWriter)
        {
            var traceId = context.Request.Headers.TryGetValue(TraceConstants.HeaderName, out var values)
                ? values.ToString()
                : null;

            // записать трейс
            traceReader.WriteValue(traceId ?? string.Empty);

            // добавить трейс в заголовок
            using (LogContext.PushProperty(TraceConstants.SerilogPropertyName, traceWriter.GetValue()))
            {
                context.Response.OnStarting(() =>
                {
                    var current = traceWriter.GetValue();
                    if (!string.IsNullOrWhiteSpace(current))
                    {
                        context.Response.Headers[TraceConstants.HeaderName] = current;
                    }

                    return Task.CompletedTask;
                });

                await _next(context);
            }
        }
    }
}