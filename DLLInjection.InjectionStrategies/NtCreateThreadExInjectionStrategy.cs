namespace DLLInjection.InjectionStrategies
{
    using DLLInjection;
    using System;
    using System.Runtime.InteropServices;

    internal class NtCreateThreadExInjectionStrategy : LoadLibraryInjectionStrategyBase
    {
        protected override unsafe IntPtr Inject(IntPtr processHandle, IntPtr loadLibraryAddress, IntPtr addressOfDllPath)
        {
            IntPtr moduleHandle = WinAPI.GetModuleHandle("ntdll.dll");
            Utils.CheckForFailure(moduleHandle == IntPtr.Zero, "Cannot load NTDLL module", new object[0]);
            IntPtr procAddress = WinAPI.GetProcAddress(moduleHandle, "NtCreateThreadEx");
            Utils.CheckForFailure(procAddress == IntPtr.Zero, "Cannot find NtCreateThreadEx address in NTDLL module", new object[0]);
            WinAPI.NtCreateThreadEx delegateForFunctionPointer = (WinAPI.NtCreateThreadEx) Marshal.GetDelegateForFunctionPointer(procAddress, typeof(WinAPI.NtCreateThreadEx));
            Utils.CheckForFailure(delegateForFunctionPointer == null, "Cannot create delegate from pointer to NtCreateThreadEx", new object[0]);
            int num = 0;
            int num2 = 0;
            WinAPI.NtCreateThreadExBuffer buffer2 = new WinAPI.NtCreateThreadExBuffer {
                Size = sizeof(WinAPI.NtCreateThreadExBuffer),
                Unknown1 = 0x10003,
                Unknown2 = 8,
                Unknown3 = new IntPtr((void*) &num2),
                Unknown4 = 0,
                Unknown5 = 0x10004,
                Unknown6 = 4,
                Unknown7 = new IntPtr((void*) &num),
                Unknown8 = 0
            };
            bool flag = Environment.Is64BitProcess;
            IntPtr zero = IntPtr.Zero;
            delegateForFunctionPointer(out zero, 0x1fffff, IntPtr.Zero, processHandle, loadLibraryAddress, addressOfDllPath, 0, 0, flag ? 0xffff : 0, flag ? 0xffff : 0, flag ? IntPtr.Zero : new IntPtr((void*) &buffer2));
            Utils.CheckForFailure(zero == IntPtr.Zero, "NtCreateThreadEx failed", new object[0]);
            return zero;
        }
    }
}

