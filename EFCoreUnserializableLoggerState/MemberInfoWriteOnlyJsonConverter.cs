using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EFCoreUnserializableLoggerState;
public class MemberInfoWriteOnlyJsonConverter<T>
        : JsonConverter<T>
    where T : MemberInfo
{
    public override bool CanConvert(Type typeToConvert)
        => typeof(T).IsAssignableFrom(typeToConvert);

    public override T? Read(
            ref Utf8JsonReader      reader,
            Type                    typeToConvert,
            JsonSerializerOptions   options)
        => throw new NotSupportedException();

    public override void Write(
            Utf8JsonWriter          writer,
            T                       value,
            JsonSerializerOptions   options)
        => writer.WriteStringValue((value is Type type)
            ? type.FullName
            : $"{value.DeclaringType}.{value.Name}");
}
