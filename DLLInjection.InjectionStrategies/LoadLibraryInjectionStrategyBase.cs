namespace DLLInjection.InjectionStrategies
{
    using DLLInjection;
    using System;
    using System.Text;

    internal abstract class LoadLibraryInjectionStrategyBase : IInjectionStrategy
    {
        protected LoadLibraryInjectionStrategyBase()
        {
        }

        public IntPtr Inject(IntPtr processHandle, string dllPath)
        {
            IntPtr ptr2;
            if (processHandle == IntPtr.Zero)
            {
                throw new ArgumentException("Invalid process handle", "processHandle");
            }
            if (string.IsNullOrWhiteSpace(dllPath))
            {
                throw new ArgumentException("Invalid dll path", "pathToDll");
            }
            byte[] bytes = Encoding.ASCII.GetBytes(dllPath + "\0");
            IntPtr lpBaseAddress = WinAPI.VirtualAllocEx(processHandle, IntPtr.Zero, (uint) bytes.Length, WinAPI.AllocationType.Reserve | WinAPI.AllocationType.Commit, WinAPI.MemoryProtection.ExecuteReadWrite);
            Utils.CheckForFailure(lpBaseAddress == IntPtr.Zero, "Cannot allocate memory in process", new object[0]);
            Utils.CheckForFailure(!WinAPI.WriteProcessMemory(processHandle, lpBaseAddress, bytes, bytes.Length, out ptr2), "Cannot write to process memory", new object[0]);
            IntPtr moduleHandle = WinAPI.GetModuleHandle("kernel32.dll");
            Utils.CheckForFailure(moduleHandle == IntPtr.Zero, "Cannot get handle to kernel32 module", new object[0]);
            IntPtr procAddress = WinAPI.GetProcAddress(moduleHandle, "LoadLibraryA");
            Utils.CheckForFailure(procAddress == IntPtr.Zero, "Cannot get address of LoadLibrary function", new object[0]);
            IntPtr ptr5 = this.Inject(processHandle, procAddress, lpBaseAddress);
            object[] args = new object[] { base.GetType().Name };
            Utils.CheckForFailure(ptr5 == IntPtr.Zero, "Cannot create remote thread using {0} method.", args);
            return ptr5;
        }

        protected abstract IntPtr Inject(IntPtr processHandle, IntPtr loadLibraryAddress, IntPtr addressOfDllPath);
    }
}

