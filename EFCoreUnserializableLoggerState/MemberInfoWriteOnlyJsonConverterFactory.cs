using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EFCoreUnserializableLoggerState;

public class MemberInfoWriteOnlyJsonConverterFactory
    : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
        => typeof(MemberInfo).IsAssignableFrom(typeToConvert);

    public override JsonConverter? CreateConverter(
            Type                    typeToConvert,
            JsonSerializerOptions   options)
        => (JsonConverter)Activator.CreateInstance(typeof(MemberInfoWriteOnlyJsonConverter<>)
            .MakeGenericType(typeToConvert))!;
}
