using CharityPlatform.SharedKernel;
using System;

namespace CharityPlatform.Domain.CharityProjectContext.Errors
{
    public class DonateOnUnavailableCharityRequestError : Error
    {
        public DonateOnUnavailableCharityRequestError()
            : base($"Donation failed. Unavailable charity request. {DateTime.Now}")
        {
        }
    }
}
