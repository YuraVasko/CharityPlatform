using CharityPlatform.Domain.OrganizationConfigurationContext.Enums;
using CharityPlatform.SharedKernel;
using System;

namespace CharityPlatform.Domain.OrganizationConfigurationContext.Events
{
    public class OrganizationUpdated : Event
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid OrganizationId { get; set; }
        public OrganizationType OrganizationType { get; set; }

        public override string EventName => nameof(OrganizationUpdated);

    }
}
