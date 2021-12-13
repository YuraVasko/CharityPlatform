using CharityPlatform.Domain.OrganizationConfigurationContext.Enums;
using CharityPlatform.SharedKernel;
using System;
using System.Collections.Generic;

namespace CharityPlatform.Domain.OrganizationConfigurationContext.Events
{
    public class OrganizationCreated : Event
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Guid> Masters { get; set; }
        public OrganizationType OrganizationType { get; set; }
        public Guid OrganizationId { get; set; }

        public override string EventName => nameof(OrganizationCreated);

        public DateTime CreationDate { get; set; }
    }
}
