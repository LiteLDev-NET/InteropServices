using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
