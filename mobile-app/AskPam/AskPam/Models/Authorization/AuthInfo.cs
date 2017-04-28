using System;
using System.Collections.Generic;

namespace AskPam.Models.Authorization
{
    public class AuthInfo
    {
        public string IdToken { get; set; }
        public Guid? OrganizationId { get; set; }
        public UserInfo UserInfoDto { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
