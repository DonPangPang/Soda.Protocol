namespace Soda.Protocol.Framework.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class ProtocolBodyLengthAttribute : Attribute
{
    public string LengthField { get; set; }

    public ProtocolBodyLengthAttribute(string lengthField)
    {
        LengthField = lengthField;
    }
}