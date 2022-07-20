using System;
using System.Runtime;
using ZeroKernel.CLibNatives;

namespace ZeroKernel
{
    unsafe class Program
    {
        static void Main() { }

        [RuntimeExport("DriverEntry")]
        static WDK.NTSTATUS DriverEntry()
        {
            var memPool = WDK.ExAllocatePool(WDK.PoolType.NonPagedPool, 0x1000);

            WDK.DbgPrintEx(0, 0, CFunctions.c_str("Hello World!"), memPool, 0);
            WDK.ExFreePool(memPool);

            return WDK.NTSTATUS.Success;
        }
    }
}
