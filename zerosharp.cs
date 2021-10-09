using System;
using System.Runtime;
using System.Runtime.InteropServices;

#region A couple very basic things
namespace System
{
    public class Object
    {
#pragma warning disable 169
        // The layout of object is a contract with the compiler.
        private IntPtr m_pMethodTable;
#pragma warning restore 169
    }
    public struct Void { }

    // The layout of primitive types is special cased because it would be recursive.
    // These really don't need any fields to work.
    public struct Boolean { }
    public struct Char { }
    public struct SByte { }
    public struct Byte { }
    public struct Int16 { }
    public struct UInt16 { }
    public struct Int32 { }
    public struct UInt32 { }
    public struct Int64 { }
    public struct UInt64 { }
    public struct IntPtr { }
    public struct UIntPtr { }
    public struct Single { }
    public struct Double { }

    public abstract class ValueType { }
    public abstract class Enum : ValueType { }

    public struct Nullable<T> where T : struct { }

    public sealed class String { public readonly int Length; }
    public abstract class Array { }
    public abstract class Delegate { }
    public abstract class MulticastDelegate : Delegate { }

    public struct RuntimeTypeHandle { }
    public struct RuntimeMethodHandle { }
    public struct RuntimeFieldHandle { }

    public class Attribute { }

    namespace Runtime.CompilerServices
    {
        public class RuntimeHelpers
        {
            public static unsafe int OffsetToStringData => sizeof(IntPtr) + sizeof(int);
        }
    }
}
namespace System.Runtime.InteropServices
{
    public sealed class DllImportAttribute : Attribute
    {
        public DllImportAttribute(string dllName) { }
    }
}
#endregion

#region Things needed by ILC
namespace System
{
    namespace Runtime
    {
        internal sealed class RuntimeExportAttribute : Attribute
        {
            public RuntimeExportAttribute(string entry) { }
        }
    }

    class Array<T> : Array { }
}

namespace Internal.Runtime.CompilerHelpers
{
    // A class that the compiler looks for that has helpers to initialize the
    // process. The compiler can gracefully handle the helpers not being present,
    // but the class itself being absent is unhandled. Let's add an empty class.
    class StartupCodeHelpers
    {
        // A couple symbols the generated code will need we park them in this class
        // for no particular reason. These aid in transitioning to/from managed code.
        // Since we don't have a GC, the transition is a no-op.
        [RuntimeExport("RhpReversePInvoke2")]
        static void RhpReversePInvoke2() { }
        [RuntimeExport("RhpReversePInvokeReturn2")]
        static void RhpReversePInvokeReturn2() { }
        [System.Runtime.RuntimeExport("__fail_fast")]
        static void FailFast() { while (true) ; }
        [System.Runtime.RuntimeExport("RhpPInvoke")]
        static void RphPinvoke() { }
        [System.Runtime.RuntimeExport("RhpPInvokeReturn")]
        static void RphPinvokeReturn() { }
    }
}
#endregion


unsafe class Program
{
    [DllImport("kernel32")]
    static extern IntPtr GetStdHandle(int nStdHandle);

    [DllImport("kernel32")]
    static extern IntPtr WriteConsoleW(IntPtr hConsole, void* lpBuffer, int charsToWrite, out int charsWritten, void* reserved);

    [RuntimeExport("Main")]
    static int Main()
    {
        //zerosharp example
        /*string hello = "Hello world!\n";
        fixed (char* pHello = hello)
        {
            WriteConsoleW(GetStdHandle(-11), pHello, hello.Length, out int _, null);
        }*/

        return 42;
    }

    [System.Runtime.RuntimeExport("MainDriverEntry")]
    static uint/*NTSTATUS*/ MainDriverEntry()
    {
        //Find a way to marshal C# string to LPCSTR without CoreCLR runtime running....
        //Marshalling string is also needed for calling imported functions from kernel32.dll
        
        //nint ntoskrnl_DbgPrintPtr = GetProcAddress(GetModuleHandleA("NtosKrnl.exe"), "DbgPrint");
        //((delegate* unmanaged[Stdcall]<[MarshalAs(UnmanagedType.LPSTR)] string, void>)ntoskrnl_DbgPrintPtr)("Hello World!");

        return 0x00000000; //NTSTAUS_SUCCESS
    }
}