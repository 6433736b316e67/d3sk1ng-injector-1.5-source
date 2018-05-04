namespace DLLInjection
{
    using System;
    using System.Runtime.CompilerServices;

    public class InjectionOptions
    {
        public bool WaitForThreadExit { get; set; }

        public static InjectionOptions Defaults =>
            new InjectionOptions();
    }
}

