using CharityPlatform.SharedKernel;
using System;

namespace CharityPlatform.Domain.OrganizationConfigurationContext.Events
{
    public class MasterRemoved : Event
    {
        public override string EventName => nameof(MasterRemoved);

        public Guid MasterId { get; set; }

        public Guid OrganizationId { get; set; }
    }
}
