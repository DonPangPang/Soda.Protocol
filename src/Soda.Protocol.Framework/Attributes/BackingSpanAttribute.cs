namespace Soda.Protocol.Framework.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class BackingSpanAttribute : Attribute
{
    public string FieldName { get; set; }

    public BackingSpanAttribute(string fieldName)
    {
        FieldName = fieldName;
    }
}