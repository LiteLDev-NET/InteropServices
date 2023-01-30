using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LiteLoader.NET.InteropServices;

public interface ICppClassHelper<T> where T : new()
{
    public struct _Value_type_funcptr_def
    {
        public unsafe static delegate*<ref T, nint> get_intptr;

        public unsafe static delegate*<ref T, nint, void> set_intptr;

        public unsafe static delegate*<ref T, bool> get_own_instance;

        public unsafe static delegate*<ref T, bool, void> set_own_instance;

        public unsafe static delegate*<ref T, nint, bool, void> set_native_pointer;

        public unsafe static delegate*<ref T, nint, bool, T> construct_instance;

        public unsafe static delegate*<ref T, T, T> ctor_copy;

        public unsafe static delegate*<ref T, move<T>, T> ctor_move;

        public unsafe static delegate*<ref T, void> dtor;
    }

    public struct _Ref_type_funcptr_def
    {
        public unsafe static delegate*<T, nint> get_intptr;

        public unsafe static delegate*<T, nint, void> set_intptr;

        public unsafe static delegate*<T, bool> get_own_instance;

        public unsafe static delegate*<T, bool, void> set_own_instance;

        public unsafe static delegate*<T, nint, bool, void> set_native_pointer;

        public unsafe static delegate*<T, nint, bool, T> construct_instance;

        public unsafe static delegate*<T, T, T> ctor_copy;

        public unsafe static delegate*<T, move<T>, T> ctor_move;

        public unsafe static delegate*<T, void> dtor;

        public unsafe static delegate*<T, T, bool> op_equality;
    }

    static readonly Type _Pointer_type;

    static readonly Type _Move_type;

    static readonly Type _ICopyable_type;

    static readonly Type _IMoveable_type;

    static ulong type_size;

    static bool isValueType;

    static bool isPointer;

    static bool isCopyable;

    static bool isMoveable;

    static bool isICppClass;

    static bool isIPointerConstructable;

    static _Value_type_funcptr_def _Tv_fptr;

    static _Ref_type_funcptr_def _Tr_fptr;

    unsafe static void* _get_method_fptr([MarshalAs(UnmanagedType.U1)] bool _ThrowExc, string _Name, Type[] _Params)
    {
        var _Fty = typeof(T).GetMethod(_Name, _Params);

        if (_Fty == null)
            goto THROW;

        var ret = _Fty.MethodHandle.GetFunctionPointer().ToPointer();

        if (ret == null)
            goto THROW;

        return ret;

    THROW:
        if (_ThrowExc)
            throw new InvalidTypeException(string.Format("{0} missing Method '{1}'.", typeof(T).FullName, _Name));
        else
            return null;
    }

    unsafe static _Value_type_funcptr_def _handle_value_type(Type _Ty)
    {
        _Value_type_funcptr_def result = default;
        type_size = (ulong)Unsafe.SizeOf<T>();
        isValueType = true;
        if (_Ty.IsGenericType)
        {
            if (_Ty.GetGenericTypeDefinition() == typeof(pointer<>))
            {
                isValueType = true;
                isPointer = true;
                Type[] @params = Array.Empty<Type>();
                string name = "get_Intptr";
                _Value_type_funcptr_def.get_intptr = (delegate*<ref T, nint>)_get_method_fptr(_ThrowExc: true, name, @params);
                Type[] array = new Type[2];
                Type type = (array[0] = typeof(nint));
                Type type2 = (array[1] = typeof(bool));
                string name2 = "SetNativePointer";
                _Value_type_funcptr_def.set_native_pointer = (delegate*<ref T, nint, bool, void>)_get_method_fptr(_ThrowExc: true, name2, array);
                return result;
            }
        }

        if (_Ty.IsAssignableTo(typeof(ICppClass)))
        {
            isICppClass = true;
            Type[] params2 = Array.Empty<Type>();
            string name3 = "get_Intptr";
            _Value_type_funcptr_def.get_intptr = (delegate*<ref T, nint>)_get_method_fptr(_ThrowExc: true, name3, params2);
            Type[] array2 = new Type[1];
            Type type3 = (array2[0] = typeof(nint));
            string name4 = "set_Intptr";
            _Value_type_funcptr_def.set_intptr = (delegate*<ref T, nint, void>)_get_method_fptr(_ThrowExc: true, name4, array2);
            Type[] params3 = Array.Empty<Type>();
            string name5 = "get_OwnsNativeInstance";
            _Value_type_funcptr_def.get_own_instance = (delegate*<ref T, bool>)_get_method_fptr(_ThrowExc: true, name5, params3);
            Type[] array3 = new Type[1];
            Type type4 = (array3[0] = typeof(bool));
            string name6 = "set_OwnsNativeInstance";
            _Value_type_funcptr_def.set_own_instance = (delegate*<ref T, bool, void>)_get_method_fptr(_ThrowExc: true, name6, array3);
            Type[] params4 = Array.Empty<Type>();
            string name7 = "Destruct";
            _Value_type_funcptr_def.dtor = (delegate*<ref T, void>)_get_method_fptr(_ThrowExc: true, name7, params4);
        }
        if (_Ty.IsAssignableTo(typeof(IPointerConstructable<T>)))
        {
            isIPointerConstructable = true;
            Type[] array4 = new Type[2];
            Type type5 = (array4[0] = typeof(nint));
            Type type6 = (array4[1] = typeof(bool));
            string name8 = "SetNativePointer";
            _Value_type_funcptr_def.set_native_pointer = (delegate*<ref T, nint, bool, void>)_get_method_fptr(_ThrowExc: true, name8, array4);

            Type[] array5 = new Type[2];
            array5[0] = typeof(nint);
            array5[1] = typeof(bool);
            string name9 = "ConstructInstance";
            _Value_type_funcptr_def.construct_instance = (delegate*<ref T, nint, bool, T>)_get_method_fptr(_ThrowExc: true, name9, array5);
        }
        Type[] typeArguments = new Type[1] { _Ty };
        if (_Ty.IsAssignableTo(_ICopyable_type.MakeGenericType(typeArguments)) && _Ty.GetConstructor(new Type[1] { _Ty }) != null)
        {
            Type[] params5 = new Type[1] { _Ty };
            string name9 = "ConstructInstanceByCopy";
            _Value_type_funcptr_def.ctor_copy = (delegate*<ref T, T, T>)_get_method_fptr(_ThrowExc: false, name9, params5);
            if (_Value_type_funcptr_def.ctor_copy != null)
            {
                isCopyable = true;
            }
        }
        Type[] typeArguments2 = new Type[1] { _Ty };
        if (_Ty.IsAssignableTo(_IMoveable_type.MakeGenericType(typeArguments2)) && _Ty.GetConstructor(new Type[1] { _Ty }) != null)
        {
            Type[] array5 = new Type[1];
            Type[] typeArguments3 = new Type[1] { _Ty };
            Type type7 = (array5[0] = _Move_type.MakeGenericType(typeArguments3));
            string name10 = "ConstructInstanceByMove";
            _Value_type_funcptr_def.ctor_move = (delegate*<ref T, move<T>, T>)_get_method_fptr(_ThrowExc: false, name10, array5);
            if (_Value_type_funcptr_def.ctor_move != null)
            {
                isMoveable = true;
            }
        }
        return result;
    }

    unsafe static _Ref_type_funcptr_def _handle_ref_type(Type _Ty)
    {
        //Discarded unreachable code: IL_02d6
        _Ref_type_funcptr_def result = default;
        if (_Ty.IsAssignableTo(typeof(IConstructableCppClass<T>)))
        {
            isICppClass = true;
            isIPointerConstructable = true;
            Type[] @params = Array.Empty<Type>();
            string name = "GetClassSize";
            type_size = ((delegate*<T, ulong>)_get_method_fptr(_ThrowExc: true, name, @params))(default!);
            Type[] params2 = Array.Empty<Type>();
            string name2 = "get_Intptr";
            _Ref_type_funcptr_def.get_intptr = (delegate*<T, nint>)_get_method_fptr(_ThrowExc: true, name2, params2);
            Type[] array = new Type[1];
            Type type = (array[0] = typeof(nint));
            string name3 = "set_Intptr";
            _Ref_type_funcptr_def.set_intptr = (delegate*<T, nint, void>)_get_method_fptr(_ThrowExc: true, name3, array);
            Type[] params3 = Array.Empty<Type>();
            string name4 = "get_OwnsNativeInstance";
            _Ref_type_funcptr_def.get_own_instance = (delegate*<T, bool>)_get_method_fptr(_ThrowExc: true, name4, params3);
            Type[] array2 = new Type[1];
            Type type2 = (array2[0] = typeof(bool));
            string name5 = "set_OwnsNativeInstance";
            _Ref_type_funcptr_def.set_own_instance = (delegate*<T, bool, void>)_get_method_fptr(_ThrowExc: true, name5, array2);
            Type[] params4 = Array.Empty<Type>();
            string name6 = "Destruct";
            _Ref_type_funcptr_def.dtor = (delegate*<T, void>)_get_method_fptr(_ThrowExc: true, name6, params4);
            Type[] array3 = new Type[2];
            Type type3 = (array3[0] = typeof(nint));
            Type type4 = (array3[1] = typeof(bool));
            string name7 = "SetNativePointer";
            _Ref_type_funcptr_def.set_native_pointer = (delegate*<T, nint, bool, void>)_get_method_fptr(_ThrowExc: true, name7, array3);
            Type[] array4 = new Type[2];
            array4[0] = typeof(nint);
            array4[1] = typeof(bool);
            string name8 = "ConstructInstance";
            _Ref_type_funcptr_def.construct_instance = (delegate*<T, nint, bool, T>)_get_method_fptr(_ThrowExc: true, name8, array4);
            Type[] typeArguments = new Type[1] { _Ty };
            if (_Ty.IsAssignableTo(_ICopyable_type.MakeGenericType(typeArguments)))
            {
                if (!(_Ty.GetConstructor(new Type[1] { _Ty }) != null))
                {
                    string text = " implements the 'ICopyable' interface but does not have the copy ctor.";
                    throw new InvalidTypeException(_Ty.FullName + text);
                }
                Type[] params5 = new Type[1] { _Ty };
                string name9 = "ConstructInstanceByCopy";
                _Ref_type_funcptr_def.ctor_copy = (delegate*<T, T, T>)_get_method_fptr(_ThrowExc: true, name9, params5);
                if (_Ref_type_funcptr_def.ctor_copy != null)
                {
                    isCopyable = true;
                }
            }
            Type[] typeArguments2 = new Type[1] { _Ty };
            if (_Ty.IsAssignableTo(_IMoveable_type.MakeGenericType(typeArguments2)))
            {
                if (!(_Ty.GetConstructor(new Type[1] { _Ty }) != null))
                {
                    string text2 = " implements the 'IMoveable' interface but does not have the move ctor.";
                    throw new InvalidTypeException(_Ty.FullName + text2);
                }
                Type[] array5 = new Type[1];
                Type[] typeArguments3 = new Type[1] { _Ty };
                Type type5 = (array5[0] = _Move_type.MakeGenericType(typeArguments3));
                string name9 = "ConstructInstanceByMove";
                _Ref_type_funcptr_def.ctor_move = (delegate*<T, move<T>, T>)_get_method_fptr(_ThrowExc: true, name9, array5);
                if (_Ref_type_funcptr_def.ctor_move != null)
                {
                    isMoveable = true;
                }
            }
            Type[] array6 = new Type[2];
            Type type6 = (array6[0] = typeof(T));
            Type type7 = (array6[1] = typeof(T));
            string name10 = "op_Equality";
            _Ref_type_funcptr_def.op_equality = (delegate*<T, T, bool>)_get_method_fptr(_ThrowExc: false, name10, array6);
            return result;
        }
        string text3 = " missing const(literal) field 'NativeClassSize'.";
        throw new InvalidTypeException(_Ty.FullName + text3);
    }

    static ICppClassHelper()
    {
        _Pointer_type = typeof(pointer<>);
        _Move_type = typeof(move<>);
        _ICopyable_type = typeof(ICopyable<>);
        _IMoveable_type = typeof(IMoveable<>);

        _Tv_fptr = default;
        _Tr_fptr = default;
        Type typeFromHandle = typeof(T);
        if (typeFromHandle.IsValueType)
        {
            _Tv_fptr = _handle_value_type(typeFromHandle);
        }
        else
        {
            _Tr_fptr = _handle_ref_type(typeFromHandle);
        }
    }
}
