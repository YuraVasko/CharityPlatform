using System;

namespace CharityPlatform.DAL.Models
{
    public class DonationRecordEntity
    {
        public Guid Id { get; set; }
        public int DonationAmount { get; set; }

        public Guid CharityProjectId { get; set; }
        public CharityProjectEntity CharityProject { get; set; }

        public Guid DonorId { get; set; }
        public DonorEntity Donor { get; set; }
    }
}
