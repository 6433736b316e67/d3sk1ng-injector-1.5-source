namespace DLLInjection
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal static class WinAPI
    {
        public const string KERNEL32_DLL = "kernel32.dll";
        public const string NTDLL_DLL = "ntdll.dll";
        public const string LOAD_LIBRARY_PROC = "LoadLibraryA";
        public const string NT_CREATE_THREAD_EX = "NtCreateThreadEx";
        public const uint INFINITE = uint.MaxValue;
        public const uint WAIT_ABANDONED = 0x80;
        public const uint WAIT_OBJECT_0 = 0;
        public const uint WAIT_TIMEOUT = 0x102;

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", SetLastError=true)]
        public static extern bool CloseHandle(IntPtr hObject);
        [DllImport("kernel32.dll")]
        public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);
        [DllImport("kernel32.dll", CharSet=CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);
        [DllImport("kernel32", CharSet=CharSet.Ansi, SetLastError=true, ExactSpelling=true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(ProcessAccessFlags processAccess, bool bInheritHandle, int processId);
        [DllImport("kernel32.dll", SetLastError=true, ExactSpelling=true)]
        public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, AllocationType flAllocationType, MemoryProtection flProtect);
        [DllImport("kernel32.dll", SetLastError=true)]
        public static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);
        [DllImport("kernel32.dll", SetLastError=true)]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, out IntPtr lpNumberOfBytesWritten);

        [Flags]
        public enum AllocationType
        {
            Commit = 0x1000,
            Reserve = 0x2000,
            Decommit = 0x4000,
            Release = 0x8000,
            Reset = 0x80000,
            Physical = 0x400000,
            TopDown = 0x100000,
            WriteWatch = 0x200000,
            LargePages = 0x20000000
        }

        [Flags]
        public enum MemoryProtection
        {
            Execute = 0x10,
            ExecuteRead = 0x20,
            ExecuteReadWrite = 0x40,
            ExecuteWriteCopy = 0x80,
            NoAccess = 1,
            ReadOnly = 2,
            ReadWrite = 4,
            WriteCopy = 8,
            GuardModifierflag = 0x100,
            NoCacheModifierflag = 0x200,
            WriteCombineModifierflag = 0x400
        }

        public delegate int NtCreateThreadEx(out IntPtr threadHandle, uint desiredAccess, IntPtr objectAttributes, IntPtr processHandle, IntPtr lpStartAddress, IntPtr lpParameter, int createSuspended, uint stackZeroBits, uint sizeOfStackCommit, uint sizeOfStackReserve, IntPtr lpBytesBuffer);

        [StructLayout(LayoutKind.Sequential, Pack=4)]
        public struct NtCreateThreadExBuffer
        {
            public int Size;
            public uint Unknown1;
            public uint Unknown2;
            public IntPtr Unknown3;
            public uint Unknown4;
            public uint Unknown5;
            public uint Unknown6;
            public IntPtr Unknown7;
            public uint Unknown8;
        }

        [Flags]
        public enum ProcessAccessFlags : uint
        {
            All = 0x1f0fff,
            Terminate = 1,
            CreateThread = 2,
            VirtualMemoryOperation = 8,
            VirtualMemoryRead = 0x10,
            VirtualMemoryWrite = 0x20,
            DuplicateHandle = 0x40,
            CreateProcess = 0x80,
            SetQuota = 0x100,
            SetInformation = 0x200,
            QueryInformation = 0x400,
            QueryLimitedInformation = 0x1000,
            Synchronize = 0x100000
        }
    }
}

