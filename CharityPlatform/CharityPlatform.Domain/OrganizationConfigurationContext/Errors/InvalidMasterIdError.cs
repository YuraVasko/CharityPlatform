using CharityPlatform.SharedKernel;

namespace CharityPlatform.Domain.OrganizationConfigurationContext.Errors
{
    public class InvalidMasterIdError : Error
    {
        public InvalidMasterIdError()
            : base("Invalid master id format")
        {
        }
    }
}
