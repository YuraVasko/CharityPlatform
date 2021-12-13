using System;
using System.Collections.Generic;

namespace CharityPlatform.DAL.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public List<CharityOrganizationEntity> CharityOrganization { get; set; }
    }
}
