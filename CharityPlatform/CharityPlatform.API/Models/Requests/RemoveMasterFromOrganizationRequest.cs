using System;

namespace CharityPlatform.API.Models.Requests
{
    public class RemoveMasterFromOrganizationRequest
    {
        public Guid OrganizationId { get; set; }

        public Guid MasterId { get; set; }
    }
}
