namespace LiteLoader.NET.InteropServices;

public interface IPointerConstructable<T> where T : new()
{
    void SetNativePointer(nint ptr, bool ownsInstance);
    T ConstructInstance(nint ptr, bool ownsInstance);
};

public interface ICppClass
{
    public abstract nint NativePointer { get; set; }
    public abstract bool OwnsNativeInstance { get; set; }

    public void Destruct();

    public ulong GetClassSize();
}

public interface IConstructableCppClass<T> : ICppClass, IPointerConstructable<T> where T : new()
{
}

public interface IAbstractCppClass : ICppClass
{
}
