using CharityPlatform.SharedKernel;
using System;

namespace CharityPlatform.Domain.CharityProjectContext.Events
{
    public class CharityRequestCreated : Event
    {
        public Guid CharityRequestId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid CharityOrganizationId { get; set; }
        public int DonationGoal { get; set; }
        public int AlreadyDonated { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public override string EventName => nameof(CharityRequestCreated);
    }
}
