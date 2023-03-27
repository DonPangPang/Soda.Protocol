namespace Soda.Protocol.Framework.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class ProtocolSkipAttribute : Attribute
{
    public int? Size { get; set; }

    public ProtocolSkipAttribute()
    { }

    public ProtocolSkipAttribute(int size)
    {
        Size = size;
    }
}