using CharityPlatform.Domain.CharityDonorsContext.Enums;
using System;
using System.Collections.Generic;

namespace CharityPlatform.DAL.Models
{
    public class DonorEntity
    {
        public Guid Id { get; set; }
        public DonorLevel DonorLevel { get; set; }
        public List<DonationRecordEntity> DonationHistory { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
