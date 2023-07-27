using Soda.Protocol.Framework.Attributes;
using Soda.Protocol.Framework.Extensions;
using Xunit.Abstractions;

namespace Soda.Protocol.Framework.Test;

public class ProtocolTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public ProtocolTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    public class SampleProtocol
    {
        [UInt16]
        public ushort Start { get; set; } = (0x79 << 8) + 0x79;

        [Bcd(10)] 
        public string MessageId { get; set; } = "111111";

        [Ascii(10)]
        public string Content { get; set; } = "FUCK";
        
        [UInt16]
        public ushort End { get; set; } = (0x79 << 8) + 0x79;
    }
    
    
    [Fact]
    public void Test1()
    {
        var data = new SampleProtocol();
        var res = ProtocolConvert.Serialize(data);
        _testOutputHelper.WriteLine(res.ToHexString());
    }

    [Fact]
    public void Test2()
    {
        var data = "797900001111114655434B0000000000007979".ToHexBytes();
        var res = ProtocolConvert.Deserialize<SampleProtocol>(data);
        
        _testOutputHelper.WriteLine($"{res?.Content}");
    }
}