using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Soda.Protocol.Framework.Attributes;

namespace Soda.Protocol.Framework.Enums
{
    [Flags]
    public enum SpanType
    {
        Auto = 0,
        Bit = 1,
        Byte = 8,
        UInt16 = 16,
        Int16 = 16,
        UInt24 = 24,
        Int24 = 24,
        UInt32 = 32,
        Int32 = 32,
        UInt64 = 64,
        Int64 = 64,

        Bits = 1000,
        Bytes = 1001,
        Bcd = 1002,
        UniCode = 1004,
        Ascii = 1008
    }
}