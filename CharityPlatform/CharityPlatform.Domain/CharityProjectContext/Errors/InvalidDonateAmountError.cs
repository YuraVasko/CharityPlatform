using CharityPlatform.SharedKernel;
using System;

namespace CharityPlatform.Domain.CharityProjectContext.Errors
{
    public class InvalidDonateAmountError : Error
    {
        public InvalidDonateAmountError(int donationAmount)
            : base($"Invalid donation amount: {donationAmount}. Donation failed. {DateTime.Now}")
        {
        }
    }
}