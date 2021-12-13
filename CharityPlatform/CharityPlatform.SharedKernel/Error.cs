using System;

namespace CharityPlatform.SharedKernel
{
    public abstract class Error : Exception
    {
        public override string Message { get; }

        public Error(string message)
        {
            Message = message;
        }
    }
}
