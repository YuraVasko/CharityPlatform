using System;

namespace CharityPlatform.DAL.Models
{
    public class CharityProjectEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid CharityOrganizationId { get; set; }
        public CharityOrganizationEntity CharityOrganization { get; set; }
        public int DonationGoal { get; set; }
        public int AlreadyDonated { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsApproved { get; set; }
        public bool IsRejected { get; set; }
    }
}
