using System;

namespace CharityPlatform.API.Models.Responses
{
    public class UserDataResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
    }
}
