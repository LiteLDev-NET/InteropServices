namespace LiteLoader.NET.InteropServices;

public interface IMoveable<TSelf> where TSelf : IMoveable<TSelf>
{
    /// <summary>
    /// static method, just invoke move_ctor and return the obj.
    /// </summary>
    TSelf ConstructInstanceByMove(move<TSelf> _Right);
}
