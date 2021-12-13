using CharityPlatform.SharedKernel;
using System;

namespace CharityPlatform.Domain.CharityProjectContext.Events
{
    public class DonateAdded : Event
    {
        public Guid CharityRequestId { get; set; }
        public Guid CharityOrganizationId { get; set; }
        public int DonateAmount { get; set; }
        public Guid DonorId { get; set; }

        public override string EventName => nameof(DonateAdded);
    }
}
