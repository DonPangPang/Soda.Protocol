namespace Soda.Protocol.Framework.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class IgnoreAttribute : Attribute
{
    public IgnoreAttribute()
    { }
}