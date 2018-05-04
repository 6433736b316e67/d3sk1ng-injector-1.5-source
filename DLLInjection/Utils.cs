namespace DLLInjection
{
    using System;
    using System.Runtime.InteropServices;

    internal static class Utils
    {
        public static void CheckForFailure(bool failureIndicator, string message, params object[] args)
        {
            if (failureIndicator)
            {
                string str = string.Format(message, args);
                string str2 = $"LastWinError: {Marshal.GetLastWin32Error()}";
                throw new DLLInjectionFailedException($"{str} ({str2})");
            }
        }
    }
}

