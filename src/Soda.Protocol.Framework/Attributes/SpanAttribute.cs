using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soda.Protocol.Framework.Enums;

namespace Soda.Protocol.Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SpanAttribute : Attribute
    {
        public SpanType SpanType { get; set; } = SpanType.Auto;

        public int? Size { get; set; }

        public SpanAttribute(SpanType spanType, int size)
        {
            SpanType = spanType;
            Size = size;
        }

        public SpanAttribute(int size)
        {
            Size = size;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class BitAttribute : Attribute
    {
        public BitAttribute(int len = 1)
        {
            Length = len;
        }

        public int Length { get; }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ByteAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class BytesAttribute : Attribute
    {
        public BytesAttribute(int len)
        {
            Length = len;
        }

        public int Length { get; }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class UInt16Attribute : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Property)]
    public class UInt24Attribute : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Property)]
    public class UInt32Attribute : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Property)]
    public class UInt64Attribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class BcdAttribute : Attribute
    {
        public BcdAttribute(int len)
        {
            Length = len;
        }

        public int Length { get; }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class CharAttribute : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Property)]
    public class BigEndianNumberAttribute : Attribute
    {
        public BigEndianNumberAttribute(int len)
        {
            Length = len;
        }

        public int Length { get; }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class AsciiAttribute : Attribute
    {
        public AsciiAttribute(int len)
        {
            Length = len;
        }

        public int Length { get; }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class UnicodeAttribute : Attribute
    {
        public UnicodeAttribute(int len)
        {
            Length = len;
        }

        public int Length { get; }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class HexAttribute : Attribute
    {
        public HexAttribute(int len)
        {
            Length = len;
        }

        public int Length { get; }
    }

    /// <summary>
    /// 六字节日期
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DateTime6Attribute : Attribute
    {

    }
}