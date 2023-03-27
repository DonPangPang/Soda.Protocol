using Soda.Protocol.Framework.Attributes;
using System.Reflection;
using System.Runtime.InteropServices;
using Soda.Protocol.Framework.Enums;

namespace Soda.Protocol.Framework.Extensions;

public static class ProtocolExtensions
{
    public static double GetInstanceSize(this object obj)
    {
        var size = 0.0;
        obj.GetInstanceSize(ref size);

        return size;
    }

    public static void GetInstanceSize(this object obj, ref double size)
    {
        var props = obj.GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);

        foreach (var prop in props)
        {
            if (prop.GetCustomAttributes<ProtocolIgnoreAttribute>(true).Any())
            {
                continue;
            }

            //if (prop.GetCustomAttributes<BackingSpanAttribute>().Any())
            //{
            //    var backingAttr = prop.GetCustomAttributes<BackingSpanAttribute>().First();
            //}

            if (prop.GetCustomAttributes<ProtocolBodyLengthAttribute>().Any())
            {
                var lengthAttr = prop.GetCustomAttribute<ProtocolBodyLengthAttribute>()!;
                var lengthProp = props.First(x => x.Name == lengthAttr.LengthField);
                var length = int.Parse(lengthProp.GetValue(obj)?.ToString() ?? "0");

                size += length;
                continue;
            }

            var attr = prop.GetCustomAttribute<ProtocolSpanAttribute>();

            if (prop.PropertyType is { IsClass: true, IsArray: false } && attr?.Size is null)
            {
                (prop.GetValue(obj) ?? Activator.CreateInstance(prop.PropertyType)!).GetInstanceSize(ref size);
                continue;
            }

            if (attr?.SpanType == SpanType.Bit || typeof(bool).IsAssignableFrom(prop.PropertyType))
            {
                size += 1 / 8.0;
                continue;
            }

            if (attr?.SpanType == SpanType.Bits)
            {
                if (attr?.Size is null) throw new ArgumentNullException();

                size += (attr.Size / 8.0) ?? 0;
                continue;
            }

            //size += attr?.Size ?? Marshal.SizeOf(prop.PropertyType);

            size += attr?.SpanType switch
            {
                SpanType.Byte => size += 1,
                SpanType.UInt16 or SpanType.Int16 => size += 2,
                SpanType.UInt24 or SpanType.Int24 => size += 3,
                SpanType.UInt32 or SpanType.Int32 => size += 4,
                SpanType.UInt64 or SpanType.Int64 => size += 8,
                SpanType.Bytes or
                SpanType.Bcd or
                SpanType.UniCode or
                SpanType.Ascii
                    => attr?.Size ?? throw new ArgumentNullException(),
                SpanType.Auto or _ => attr?.Size ?? Marshal.SizeOf(prop.PropertyType),
            };
        }
    }
}