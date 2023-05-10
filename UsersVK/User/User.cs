using Microsoft.AspNetCore.Identity;
using System;

namespace UsersVK.Models
{
    public class User : IdentityUser
    {
        public override string Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public UserGroup UserGroupId { get; set; }
        public UserState UserStateId { get; set; }
    }
}
