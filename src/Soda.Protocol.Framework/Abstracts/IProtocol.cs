namespace Soda.Protocol.Framework.Abstracts;

public interface IProtocol
{
}

public interface IProtocol<out T> where T : Enum
{
    public T MessageId { get; }
}