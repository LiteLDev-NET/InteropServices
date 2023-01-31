namespace LiteLoader.NET.InteropServices;

public interface ICopyable<TSelf> where TSelf : ICopyable<TSelf>
{
    /// <summary>
    /// static method, just invoke copy_ctor and return the obj.
    /// </summary>
    TSelf ConstructInstanceByCopy(TSelf _Right);
}
