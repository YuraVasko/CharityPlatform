using System;

namespace CharityPlatform.API.Models.Requests
{
    public class CreateCharityProjectRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid CharityOrganizationId { get; set; }
        public int DonationGoal { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
