namespace DLLInjection.InjectionStrategies
{
    using DLLInjection;
    using System;

    internal class CreateRemoteThreadInjectionStrategy : LoadLibraryInjectionStrategyBase
    {
        protected override IntPtr Inject(IntPtr processHandle, IntPtr loadLibraryAddress, IntPtr addressOfDllPath)
        {
            IntPtr ptr = WinAPI.CreateRemoteThread(processHandle, IntPtr.Zero, 0, loadLibraryAddress, addressOfDllPath, 0, IntPtr.Zero);
            Utils.CheckForFailure(ptr == IntPtr.Zero, "Cannot create remote thread using CreateRemoteThread method", new object[0]);
            return ptr;
        }
    }
}

