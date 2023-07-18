using Soda.Protocol.Framework.Attributes;
using Soda.Protocol.Framework.Enums;
using Soda.Protocol.Framework.Extensions;

namespace Soda.Protocol.Framework.Test
{
    public class ProtocolExtensionTest
    {
        private class MyClass
        {
            public ushort Header { get; set; }

            [Span(30)]
            public byte[] Data { get; set; } = Array.Empty<byte>();

            [Span(SpanType.Bits, 4)]
            public byte End { get; set; }
        }

        [Fact]
        public void Test1()
        {
            var size = new MyClass().GetInstanceSize();

            Assert.Equal(32.5, size);
        }

        private class MyClass2
        {
            public ushort Header { get; set; }

            [Span(30)]
            public byte[] Data { get; set; } = Array.Empty<byte>();

            [Span(SpanType.Bits, 4)]
            public byte End { get; set; }

            public MyClass? Child { get; set; }

            public bool IsAck { get; set; }

            [Ignore]
            public bool IsReq { get; set; }
        }

        [Fact]
        public void Test2()
        {
            var size = new MyClass2().GetInstanceSize();

            Assert.Equal(65.125, size);
        }

        private class MyClass3
        {
            public ushort Header { get; set; } = 5;

            [Span(30)]
            public byte[] Data { get; set; } = Array.Empty<byte>();

            [Span(SpanType.Bits, 4)]
            public byte End { get; set; }

            [Span(20)]
            public MyClass? Child { get; set; }

            public bool IsAck { get; set; }

            [Ignore]
            public bool IsReq { get; set; }

            [BodyLength(nameof(Header))]
            public byte[] Body { get; set; } = Array.Empty<byte>();
        }

        [Fact]
        public void Test3()
        {
            var size = (new MyClass3()).GetInstanceSize();

            Assert.Equal(57.625, size);
        }
    }
}