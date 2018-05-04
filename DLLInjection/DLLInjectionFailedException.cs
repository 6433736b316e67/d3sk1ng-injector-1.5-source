namespace DLLInjection
{
    using System;

    [Serializable]
    public class DLLInjectionFailedException : Exception
    {
        public DLLInjectionFailedException(string message) : base(message)
        {
        }
    }
}

