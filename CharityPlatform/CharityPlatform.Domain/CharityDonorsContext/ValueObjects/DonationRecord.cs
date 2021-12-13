using System;

namespace CharityPlatform.Domain.CharityDonorsContext.ValueObjects
{
    public class DonationRecord
    {
        public int DonationAmount { get; }

        public Guid CharityProjectId { get; }

        public DonationRecord(int donationAmount, Guid charityProjectId)
        {
            CharityProjectId = charityProjectId;
            DonationAmount = donationAmount;
        }
    }
}
