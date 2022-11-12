using ZeroKernel.CLibNatives;
using System.Runtime;
using System.Runtime.InteropServices;

namespace ZeroKernel
{
    public unsafe class Program
    {
        static void Main() { }

        [RuntimeExport("wmainCRTStartup")]
        static void wmainCRTStartup()
        {

        }

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