namespace LiteLoader.NET.InteropServices;


public unsafe struct reference<T> : IConstructableCppClass<reference<T>> where T : IConstructableCppClass<T>, new()
{
    private nint ptr;

    internal reference(nint p)
    {
        ptr = p;
    }

    public nint NativePointer { get => ptr; set => ptr = value; }
    public bool OwnsNativeInstance { get { return false; } set { return; } }

    public static implicit operator reference<T>(T val)
    {
        return new reference<T>(val.NativePointer);
    }

    public static implicit operator T(reference<T> val)
    {
        return val.Dereference();
    }

    public reference<T> ConstructInstance(nint ptr, bool ownsInstance)
    {
        var ret = default(reference<T>);
        ret.ptr = ptr;
        return ret;
    }
    public void Destruct()
    {
        return;
    }

    public ulong GetClassSize()
    {
        return 8;
    }

    public void SetNativePointer(nint ptr, bool ownsInstance)
    {
        this.ptr = ptr;
    }

    private static T _EmptyTval = default!;

    public T Dereference()
    {
        if (ICppClassHelper<T>.isValueType)
        {
            return ICppClassHelper<T>._Value_type_funcptr_def.construct_instance(ref _EmptyTval, ptr, false);
        }
        else
        {
            return ICppClassHelper<T>._Ref_type_funcptr_def.construct_instance(_EmptyTval, ptr, false);
        }
    }
}