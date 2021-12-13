using CharityPlatform.SharedKernel;

namespace CharityPlatform.Domain.OrganizationConfigurationContext.Errors
{
    public class InvalidOrganizationDescriptionError : Error
    {
        public InvalidOrganizationDescriptionError()
            : base("Invalid organization description format")
        {
        }
    }
}
