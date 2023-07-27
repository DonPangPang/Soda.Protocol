namespace Soda.Protocol.Framework.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class SkipAttribute : Attribute
{
    public SkipAttribute(int len = 1)
    {
        Length = len;
    }

    public int Length { get; }
}