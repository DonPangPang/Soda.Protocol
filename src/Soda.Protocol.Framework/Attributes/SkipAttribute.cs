namespace Soda.Protocol.Framework.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class SkipAttribute : Attribute
{
    public int? Size { get; set; }

    public SkipAttribute()
    { }

    public SkipAttribute(int size)
    {
        Size = size;
    }
}