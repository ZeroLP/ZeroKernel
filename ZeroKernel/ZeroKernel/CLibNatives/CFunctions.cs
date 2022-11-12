using System;

namespace ZeroKernel.CLibNatives
{
    internal unsafe class CFunctions
    {
        //https://github.com/VollRagm/KernelSharp/blob/743fa03e35ed98bac46bf9200d80762016cd908d/KernelSharp/WDK.cs#L81
        public static char* c_str(string str)
        {
            fixed (void* wc = str)
            {
                //Allocate pool for char* taking the null terminator into consideration
                var buf = WDK.ExAllocatePool(WDK.PoolType.NonPagedPool, (ulong)str.Length + 1);

                //convert wchar_t* to char*
                WDK.wcstombs((char*)buf, wc, (ulong)(str.Length * 2) + 2);
                return (char*)buf;
            }
        }

        //https://github.com/VollRagm/KernelSharp/blob/743fa03e35ed98bac46bf9200d80762016cd908d/KernelSharp/WDK.cs#L94
        public static ulong __readcr3()
        {
            void* buffer = stackalloc byte[0x5C0];
            var sat = WDK.KeSaveStateForHibernate(buffer);
            ulong cr3 = *(ulong*)((ulong)buffer + 0x10);
            return cr3;
        }
    }
}
