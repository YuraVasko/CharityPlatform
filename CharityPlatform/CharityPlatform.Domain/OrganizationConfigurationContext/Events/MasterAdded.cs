using CharityPlatform.SharedKernel;
using System;

namespace CharityPlatform.Domain.OrganizationConfigurationContext.Events
{
    public class MasterAdded : Event
    {
        public override string EventName => nameof(MasterAdded);

        public Guid MasterId { get; set; }

        public Guid OrganizationId { get; set; }
    }
}
