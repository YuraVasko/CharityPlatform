using CharityPlatform.SharedKernel;
using System;

namespace CharityPlatform.Domain.CharityDonorsContext.Errors
{
    public class InvalidDonationAmountError : Error
    {
        public InvalidDonationAmountError(int donationAmount, Guid donorId) 
            : base($"Invalid donation amount: {donationAmount}. For donor: {donorId}.")
        {
        }
    }
}
