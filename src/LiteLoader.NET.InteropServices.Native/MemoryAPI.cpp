#include <memory>

namespace LiteLoader::NET::InteropServices::Native
{
    public ref class MemoryAPI abstract sealed
    {
    public:
        static void* operator_new(size_t size)
        {
            return ::operator new(size);
        }
        static void operator_delete(void* ptr)
        {
            ::operator delete(ptr);
        }
        static void operator_delete(void** ptr)
        {
            if ((*ptr) == nullptr)
                return;
            else
            {
                ::operator delete(*ptr);
                *ptr = nullptr;
            }
        }
        static void operator_delete_array(void* ptr)
        {
            ::operator delete[](ptr);
        }
        static void operator_delete_array(void** ptr)
        {
            if ((*ptr) == nullptr)
                return;
            else
            {
                ::operator delete[](*ptr);
                *ptr = nullptr;
            }
        }
    };
}