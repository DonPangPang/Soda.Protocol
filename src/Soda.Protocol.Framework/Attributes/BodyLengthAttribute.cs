namespace Soda.Protocol.Framework.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class BodyLengthAttribute : Attribute
{
    public string LengthField { get; set; }

    public BodyLengthAttribute(string lengthField)
    {
        LengthField = lengthField;
    }
}