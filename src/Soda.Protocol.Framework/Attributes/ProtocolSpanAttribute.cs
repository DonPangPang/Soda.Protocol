using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soda.Protocol.Framework.Enums;

namespace Soda.Protocol.Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ProtocolSpanAttribute : Attribute
    {
        public SpanType SpanType { get; set; } = SpanType.Auto;

        public int? Size { get; set; }

        public ProtocolSpanAttribute(SpanType spanType, int size)
        {
            SpanType = spanType;
            Size = size;
        }

        public ProtocolSpanAttribute(int size)
        {
            Size = size;
        }
    }
}