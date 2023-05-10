using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersVK.Models
{
    public class PrintClass
    {
        public string Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid GroupCodeId { get; set; }
        public GroupEnum GroupCode { get; set; }
        public string GroupDescription { get; set; }
        public Guid StateCodeId { get; set; }
        public StateEnum StateCode { get; set; }
        public string StateDescription { get; set; }

        public PrintClass(User user, UserGroup userGroup, UserState userState)
        {
            Id = user.Id;
            Login = user.Login;
            Password = user.Password;
            CreatedDate = user.CreatedDate;
            GroupCodeId = user.UserGroupId;
            GroupCode = userGroup.Code;
            GroupDescription = userGroup.Description;
            StateCodeId = user.UserStateId;
            StateCode = userState.Code;
            StateDescription = userState.Description;
        }
    }
}
