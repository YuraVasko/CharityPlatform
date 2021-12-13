using System;

namespace CharityPlatform.API.Models.Requests
{
    public class AddDonationRequest
    {
        public int Amount { get; set; }
        public Guid DonorId { get; set; }
        public Guid CharityRequestId { get; set; }
    }
}
