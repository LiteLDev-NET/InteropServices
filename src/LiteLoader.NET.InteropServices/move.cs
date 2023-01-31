using System.Runtime.CompilerServices;

namespace LiteLoader.NET.InteropServices
{
    public struct move<T>
    {
        public T instance;

        public move(T instance)
        {
            this.instance = instance;
        }

        public static implicit operator T(move<T> v)
        {
            return v.instance;
        }

        public static explicit operator move<T>(T v)
        {
            Unsafe.SkipInit(out move<T> result);
            result.instance = v;
            return result;
        }

        public T Get()
        {
            return instance;
        }
    }
}
