using CharityPlatform.SharedKernel;
using System;

namespace CharityPlatform.Domain.CharityProjectContext.Errors
{
    public class InvalidRequestTitleFormatError : Error
    {
        public InvalidRequestTitleFormatError()
            : base($"Invalid request title format error. Creation failed. {DateTime.Now}")
        {
        }
    }
}