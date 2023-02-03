using System.Runtime.CompilerServices;

namespace LiteLoader.NET.InteropServices.Gsl;


public unsafe struct not_null<T> : IConstructableCppClass<not_null<T>> where T : IConstructableCppClass<T>, new()
{
    private nint ptr;

    internal not_null(nint p)
    {
        ptr = p;
    }

    public nint NativePointer { get => ptr; set => ptr = value; }
    public bool OwnsNativeInstance { get { return true; } set { return; } }

    public static implicit operator not_null<T>(T val)
    {
        return new not_null<T>(val.NativePointer);
    }

    public static explicit operator T(not_null<T> val)
    {
        return val.Dereference();
    }

    public not_null<T> ConstructInstance(nint ptr, bool ownsInstance)
    {
        var ret = default(not_null<T>);
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
            if (ICppClassHelper<T>.isICppClass)
            {
                return ICppClassHelper<T>._Value_type_funcptr_def.construct_instance(ref _EmptyTval, Unsafe.Read<nint>(ptr.ToPointer()), false);
            }
            return Unsafe.Read<T>(ptr.ToPointer());
        }
        else
        {
            return ICppClassHelper<T>._Ref_type_funcptr_def.construct_instance(_EmptyTval, Unsafe.Read<nint>(ptr.ToPointer()), false);
        }
    }
}
