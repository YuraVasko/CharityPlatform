using CharityPlatform.Domain.CharityDonorsContext.Enums;
using CharityPlatform.Domain.CharityDonorsContext.ValueObjects;
using CharityPlatform.SharedKernel;
using System;
using System.Collections.Generic;

namespace CharityPlatform.Domain.CharityDonorsContext.Events
{
    public class DonorCreated : Event
    {
        public Guid UserId { get; set; }
        public Guid DonorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DonorLevel DonorLevel { get; set; }
        public List<DonationRecord> DonationHistory { get; set; }
        public override string EventName => nameof(DonorCreated);
    }
}
