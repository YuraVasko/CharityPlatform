using CharityPlatform.SharedKernel;
using System;

namespace CharityPlatform.Domain.CharityDonorsContext.Events
{
    public class DonationHistoryAdded : Event
    {
        public Guid DonorId { get; set; }
        public int DonationAmount { get; set; }

        public Guid CharityProjectId { get; set; }

        public override string EventName => nameof(DonationHistoryAdded);
    }
}
