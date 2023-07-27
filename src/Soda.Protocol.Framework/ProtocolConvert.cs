using System.Collections;
using System.Reflection;
using Soda.Protocol.Framework.Attributes;
using Soda.Protocol.Framework.Core;

namespace Soda.Protocol.Framework;

public static class ProtocolConvert
{
    public static T? Deserialize<T>(byte[] data) where T : new()
    {
        var reader = new MessagePackReader(data);

        var res = Deserialize(typeof(T), ref reader);
        
        return (T?)res;
    }

    private static object? Deserialize(Type type, ref MessagePackReader reader)
    {
        var res = Activator.CreateInstance(type);

        foreach (var prop in type.GetProperties().Where(x => !x.GetCustomAttributes<IgnoreAttribute>().Any()))
        {
            if (!IsValue(prop.PropertyType) && prop.PropertyType.IsClass)
            {
                prop.SetValue(res, Deserialize(prop.PropertyType, ref reader));
            }
            
            var attr = prop.GetCustomAttributes().FirstOrDefault();

            if (attr is SkipAttribute skipAttribute)
            {
                reader.Skip(skipAttribute.Length);

                continue;
            }

            object value = attr switch
            {
                AsciiAttribute asciiAttribute => reader.ReadAscii(asciiAttribute.Length),
                BcdAttribute bcdAttribute => reader.ReadBcd(bcdAttribute.Length),
                BigEndianNumberAttribute bigEndianNumberAttribute => reader.ReadBigNumber(bigEndianNumberAttribute
                    .Length),
                //BitAttribute bitAttribute => throw new NotImplementedException(),
                //BodyLengthAttribute bodyLengthAttribute => throw new NotImplementedException(),
                ByteAttribute byteAttribute => reader.ReadByte(),
                BytesAttribute bytesAttribute => reader.ReadArray(bytesAttribute.Length).ToArray(),
                CharAttribute charAttribute => reader.ReadChar(),
                DateTime6Attribute dateTime6Attribute => reader.ReadDateTime6() ?? default,
                HexAttribute hexAttribute => reader.ReadHex(hexAttribute.Length),
                UInt16Attribute uInt16Attribute => reader.ReadUInt16(),
                UInt24Attribute uInt24Attribute => reader.ReadUInt24(),
                UInt32Attribute uInt32Attribute => reader.ReadUInt32(),
                UInt64Attribute uInt64Attribute => reader.ReadUInt64(),
                UnicodeAttribute unicodeAttribute => reader.ReadUnicode(unicodeAttribute.Length),
                _ => throw new ArgumentOutOfRangeException(nameof(prop.PropertyType.Name))
            };

            prop.SetValue(res, value);
        }


        return res;
    }

    public static byte[] Serialize<T>(T obj, int minBufferSize = 4096)
    {
        byte[] buffer = SodaArrayPool.Rent(minBufferSize);
        try
        {
            MessagePackWriter writer = new MessagePackWriter(buffer);
            Serialize(obj, ref writer);
            return writer.FlushAndGetEncodingArray();
        }
        finally
        {
            SodaArrayPool.Return(buffer);
        }
    }

    private static void Serialize<T>(T obj, ref MessagePackWriter writer)
    {
        foreach (var prop in typeof(T).GetProperties().Where(x => !x.GetCustomAttributes<IgnoreAttribute>().Any()))
        {
            if (!IsValue(prop.PropertyType))
            {
                Serialize(prop.GetValue(obj), ref writer);
                
                continue;
            }
            
            var attr = prop.GetCustomAttributes().FirstOrDefault();

            if (attr is SkipAttribute skipAttribute)
            {
                continue;
            }

            var value = prop.GetValue(obj);

            if (value is null)
            {
                throw new ArgumentNullException(nameof(prop.PropertyType.Name));
            }

            switch (attr)
            {
                case AsciiAttribute asciiAttribute:
                    break;
            }

            switch (attr)
            {
                case AsciiAttribute asciiAttribute:
                    writer.WriteAscii((string)value, asciiAttribute.Length);
                    break;
                case BcdAttribute bcdAttribute:
                    writer.WriteBcd((string)value, bcdAttribute.Length);
                    break;
                case BigEndianNumberAttribute bigEndianNumberAttribute:
                    writer.WriteBigNumber((string)value, bigEndianNumberAttribute.Length);
                    break;
                //BitAttribute bitAttribute : throw new NotImplementedException();break;
                //BodyLengthAttribute bodyLengthAttribute : throw new NotImplementedException();break;
                case ByteAttribute byteAttribute:
                    writer.WriteByte((byte)value);
                    break;
                case BytesAttribute bytesAttribute:
                    writer.WriteArray((byte[])value);
                    break;
                case CharAttribute charAttribute:
                    writer.WriteChar((char)value);
                    break;
                case DateTime6Attribute dateTime6Attribute:
                    writer.WriteDateTime6((DateTime)value);
                    break;
                case HexAttribute hexAttribute:
                    writer.WriteHex((string)value, hexAttribute.Length);
                    break;
                case UInt16Attribute uInt16Attribute:
                    writer.WriteUInt16((UInt16)value);
                    break;
                case UInt24Attribute uInt24Attribute:
                    writer.WriteUInt24((UInt24)value);
                    break;
                case UInt32Attribute uInt32Attribute:
                    writer.WriteUInt32((UInt32)value);
                    break;
                case UInt64Attribute uInt64Attribute:
                    writer.WriteUInt64((UInt64)value);
                    break;
                case UnicodeAttribute unicodeAttribute:
                    writer.WriteUniCode((string)value);
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(prop.PropertyType.Name));
            }
        }
    }
    
    private static bool IsValue(Type type)
    {
        return type.IsValueType ||
               type.IsArray ||
               type == typeof(string) ||
               type == typeof(bool) ||
               type == typeof(int) ||
               type == typeof(double) ||
               type == typeof(float) ||
               type == typeof(DateTime);
    }

    private static bool IsArray(Type type, out Type? innerType)
    {
        if (IsValue(type))
        {
            innerType = null;
            return false;
        }

        if (type.IsArray)
        {
            innerType = type.GetElementType();
            return innerType is not null;
        }

        if (typeof(IEnumerable).IsAssignableFrom(type) ||
            typeof(IList).IsAssignableFrom(type) ||
            typeof(ICollection).IsAssignableFrom(type))
        {
            var types = type.GetGenericArguments();
            innerType = types.Any() ? types[0] : null;

            return innerType != null;
        }

        innerType = null;
        return false;
    }
}