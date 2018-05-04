namespace DLLInjection
{
    using DLLInjection.InjectionStrategies;
    using System;
    using System.IO;
    using System.Runtime.InteropServices;

    public class DLLInjector
    {
        private IInjectionStrategy _injectionStrategy;

        public DLLInjector(InjectionMethod injectionMethod)
        {
            this._injectionStrategy = InjectionStrategyFactory.Create(injectionMethod);
        }

        public void Inject(int pid, string pathToDll, InjectionOptions injectionOptions = null)
        {
            if (pid <= 0)
            {
                throw new ArgumentException("Invalid process pid: " + pid, "pid");
            }
            if (string.IsNullOrWhiteSpace(pathToDll) || !File.Exists(pathToDll))
            {
                throw new ArgumentException($"Cannot access DLL: "{pathToDll}"");
            }
            if (injectionOptions == null)
            {
            }
            injectionOptions = InjectionOptions.Defaults;
            IntPtr processHandle = WinAPI.OpenProcess(WinAPI.ProcessAccessFlags.CreateThread | WinAPI.ProcessAccessFlags.QueryInformation | WinAPI.ProcessAccessFlags.VirtualMemoryOperation | WinAPI.ProcessAccessFlags.VirtualMemoryRead | WinAPI.ProcessAccessFlags.VirtualMemoryWrite, false, pid);
            object[] args = new object[] { pid };
            Utils.CheckForFailure(processHandle == IntPtr.Zero, "Cannot open process with PID: {0}", args);
            IntPtr hHandle = this._injectionStrategy.Inject(processHandle, pathToDll);
            if (injectionOptions.WaitForThreadExit)
            {
                WinAPI.WaitForSingleObject(hHandle, uint.MaxValue);
            }
            WinAPI.CloseHandle(processHandle);
        }
    }
}

