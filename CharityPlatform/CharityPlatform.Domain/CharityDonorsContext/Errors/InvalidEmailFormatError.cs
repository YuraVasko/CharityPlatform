using CharityPlatform.SharedKernel;
using System;

namespace CharityPlatform.Domain.CharityDonorsContext.Errors
{
    public class InvalidEmailFormatError : Error
    {
        public InvalidEmailFormatError()
            : base($"Invalid email format. Donor creation failed. Creation time: {DateTime.Now}")
        {
        }
    }
}
