using CharityPlatform.SharedKernel;
using System;

namespace CharityPlatform.Domain.CharityProjectContext.Events
{
    public class CharityRequestApproved : Event
    {
        public Guid CharityRequestId { get; set; }
        public override string EventName => nameof(CharityRequestApproved);
    }
}
