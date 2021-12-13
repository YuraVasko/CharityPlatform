using CharityPlatform.Domain.CharityDonorsContext.Enums;
using CharityPlatform.SharedKernel;
using System;

namespace CharityPlatform.Domain.CharityDonorsContext.Events
{
    public class DonorLevelUpdated : Event
    {
        public Guid DonorId { get; set; }
        public DonorLevel DonorLevelToUpdate { get; set; }
        public override string EventName => nameof(DonorLevelUpdated);
    }
}
