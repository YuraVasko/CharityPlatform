using CharityPlatform.SharedKernel;

namespace CharityPlatform.Domain.OrganizationConfigurationContext.Errors
{
    public class InvalidOrganizationNameError : Error
    {
        public InvalidOrganizationNameError()
            : base("Invalid organization name format")
        {
        }
    }
}
