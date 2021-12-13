using System;

namespace CharityPlatform.API.Models.Requests
{
    public class AddMasterToOrganizationRequest
    {
        public Guid OrganizationId { get; set; }

        public Guid MasterId { get; set; }
    }
}
