using CharityPlatform.Domain.OrganizationConfigurationContext.Enums;
using System;
using System.Collections.Generic;

namespace CharityPlatform.DAL.Models
{
    public class CharityOrganizationEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public OrganizationType OrganizationType { get; set; }
        public DateTime CreationDate { get; set; }
        public List<User> Masters { get; set; }
    }
}
