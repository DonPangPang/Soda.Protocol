namespace Soda.Protocol.Framework.Abstracts;

public interface IProtocol
{
}

/// <summary>
/// 结束符协议
/// </summary>
public interface ITerminatorProtocol<T> : IProtocol
{
    T End { get; set; }
}

/// <summary>
/// 固定数量分隔符协议
/// </summary>
public interface ICountSpliterProtocol<T> : IProtocol
{
    T Size { get; set; }
}

/// <summary>
/// 固定请求大小协议
/// </summary>
public interface IFixedSizeProtocol<T> : IProtocol
{
    T Size { get; set; }
}

/// <summary>
/// 带起止符的协议
/// </summary>
public interface IBeginEndMarkProtocol<T> : IBeginEndMarkProtocol<T, T>
{
    
}

public interface IBeginEndMarkProtocol<TStart, TEnd> : IProtocol
{
    TStart Start { get; set; }
    TEnd End { get; set; }
}

/// <summary>
/// 头部格式固定且包含内容长度的协议
/// </summary>
public interface IFixedHeaderProtocol<T> : IProtocol
{
    IFixedHeaderProtocolHeader<T> Header { get; set; }
}

public interface IFixedHeaderProtocolHeader<T>:IProtocol
{
    
}