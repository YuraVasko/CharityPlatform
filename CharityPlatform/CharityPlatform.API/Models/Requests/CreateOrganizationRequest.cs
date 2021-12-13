using System;

namespace CharityPlatform.API.Models.Requests
{
    public class CreateOrganizationRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid Master { get; set; }
    }
}
