using CharityPlatform.SharedKernel;
using System;

namespace CharityPlatform.Domain.CharityDonorsContext.Errors
{
    public class InvalidLastNameFormatError : Error
    {
        public InvalidLastNameFormatError()
            : base($"Invalid last name format. Donor creation failed. Creation time: {DateTime.Now}")
        {
        }
    }
}