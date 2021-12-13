using CharityPlatform.SharedKernel;
using System;

namespace CharityPlatform.Domain.CharityDonorsContext.Errors
{
    public class InvalidFirsNameFormatError : Error
    {
        public InvalidFirsNameFormatError()
            : base($"Invalid first name format. Donor creation failed. Creation time: {DateTime.Now}")
        {
        }
    }
}