using CharityPlatform.SharedKernel;
using System;

namespace CharityPlatform.Domain.CharityDonorsContext.Errors
{
    public class InvalidDonorStateError : Error
    {
        public InvalidDonorStateError(Guid donorId)
            : base($"Invalid donor state. Upgrade is failed. DonorId: {donorId}.")
        {
        }
    }
}
