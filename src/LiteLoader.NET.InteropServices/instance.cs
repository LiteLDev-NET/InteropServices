﻿using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LiteLoader.NET.InteropServices;

[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 8)]
public unsafe struct instance<T> : IConstructableCppClass<instance<T>> where T : IConstructableCppClass<T>, new()
{
    [FieldOffset(0)]
    private nint ptr;

    internal instance(nint p)
    {
        ptr = p;
    }

    public nint NativePointer { get => ptr; set => ptr = value; }
    public bool OwnsNativeInstance { get { return true; } set { return; } }

    public static implicit operator instance<T>(T val)
    {
        return new instance<T>(val.NativePointer);
    }

    public static implicit operator T(instance<T> val)
    {
        return val.Get();
    }

    public instance<T> ConstructInstance(nint ptr, bool ownsInstance)
    {
        var ret = default(instance<T>);
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

    public T Get()
    {
        if (ptr != -1)
        {
            if (ICppClassHelper<T>.isValueType)
            {
                ptr = -1;
                return ICppClassHelper<T>._Value_type_funcptr_def.construct_instance(ref _EmptyTval, ptr, true);
            }
            else
            {
                ptr = -1;
                return ICppClassHelper<T>._Ref_type_funcptr_def.construct_instance(_EmptyTval, ptr, true);
            }
        }
        else
        {
            throw new InvalidOperationException();
        }
    }
}
