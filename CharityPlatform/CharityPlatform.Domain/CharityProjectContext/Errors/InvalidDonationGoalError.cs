using CharityPlatform.SharedKernel;
using System;

namespace CharityPlatform.Domain.CharityProjectContext.Errors
{
    public class InvalidDonationGoalError : Error
    {
        public InvalidDonationGoalError(int donationGoal)
            : base($"Invalid donation goal: {donationGoal}. Creation failed. {DateTime.Now}")
        {
        }
    }
}