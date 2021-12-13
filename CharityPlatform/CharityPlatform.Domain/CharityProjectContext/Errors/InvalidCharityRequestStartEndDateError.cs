using CharityPlatform.SharedKernel;
using System;

namespace CharityPlatform.Domain.CharityProjectContext.Errors
{
    public class InvalidCharityRequestStartEndDateError : Error
    {
        public InvalidCharityRequestStartEndDateError()
            : base($"Invalid charity reuqest start-end dates. Creation failed. {DateTime.Now}")
        {
        }
    }
}