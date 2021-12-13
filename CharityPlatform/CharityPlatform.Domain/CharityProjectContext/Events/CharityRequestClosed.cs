using CharityPlatform.SharedKernel;
using System;

namespace CharityPlatform.Domain.CharityProjectContext.Events
{
    public class CharityRequestClosed : Event
    {
        public Guid CharityRequestId { get; set; }

        public DateTime EndDate { get; set; }

        public override string EventName => nameof(CharityRequestClosed);
    }
}
