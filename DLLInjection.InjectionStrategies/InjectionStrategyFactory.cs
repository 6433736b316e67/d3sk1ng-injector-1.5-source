namespace DLLInjection.InjectionStrategies
{
    using DLLInjection;
    using System;

    internal static class InjectionStrategyFactory
    {
        public static IInjectionStrategy Create(InjectionMethod injectionMethod)
        {
            if (injectionMethod != InjectionMethod.CREATE_REMOTE_THREAD)
            {
                if (injectionMethod != InjectionMethod.NT_CREATE_THREAD_EX)
                {
                    throw new NotSupportedException($"Injection strategy: {injectionMethod} is not supported");
                }
                return new NtCreateThreadExInjectionStrategy();
            }
            return new CreateRemoteThreadInjectionStrategy();
        }
    }
}

