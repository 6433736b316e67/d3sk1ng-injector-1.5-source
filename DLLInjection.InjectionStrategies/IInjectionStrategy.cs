namespace DLLInjection.InjectionStrategies
{
    using System;

    internal interface IInjectionStrategy
    {
        IntPtr Inject(IntPtr processHandle, string dllPath);
    }
}

