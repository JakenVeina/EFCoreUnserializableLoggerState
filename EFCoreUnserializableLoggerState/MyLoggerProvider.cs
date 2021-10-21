using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.Extensions.Logging;

namespace EFCoreUnserializableLoggerState;

public sealed class MyLoggerProvider
    : ILoggerProvider
{
    public MyLoggerProvider()
    {
        _options = new JsonSerializerOptions()
        {
            ReferenceHandler = ReferenceHandler.Preserve
        };
        _options.Converters.Add(new MemberInfoWriteOnlyJsonConverterFactory());
    }

    public ILogger CreateLogger(string categoryName)
        => new MyLogger(_options);

    public void Dispose() { }

    private readonly JsonSerializerOptions _options;

    private class MyLogger
        : ILogger
    {
        public MyLogger(JsonSerializerOptions options)
            => _options = options;

        public IDisposable BeginScope<TState>(TState state)
            => EmptyScope.Instance;

        public bool IsEnabled(LogLevel logLevel)
            => logLevel is LogLevel.Error;

        public void Log<TState>(
            LogLevel                            logLevel,
            EventId                             eventId,
            TState                              state,
            Exception?                          exception,
            Func<TState, Exception?, string>    formatter)
        {
            if (eventId.Id != _saveChangesFailedEventId)
                return;

            Console.WriteLine($"SaveChangesFailed: {JsonSerializer.Serialize(state, _options)}");
        }

        private static readonly EventId _saveChangesFailedEventId
            = new(10000, "Microsoft.EntityFrameworkCore.Update.SaveChangesFailed");

        private readonly JsonSerializerOptions _options;

        private sealed class EmptyScope
            : IDisposable
        {
            public static readonly EmptyScope Instance
                = new();

            private EmptyScope() { }

            public void Dispose() { }
        }
    }
}
