using CharityPlatform.SharedKernel;
using System;

namespace CharityPlatform.Domain.CharityProjectContext.Errors
{
    public class InvalidRequestDescriptionFormatError : Error
    {
        public InvalidRequestDescriptionFormatError()
            : base($"Invalid request description format error. Creation failed. {DateTime.Now}")
        {
        }
    }
}
