using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersVK.Models
{
    public enum GroupEnum
    {
        Admin,
        User
    }
    public class UserGroup
    {
        public Guid Id { get; set; }
        public GroupEnum Code { get; set; }
        public string Description { get; set; }
    }
}
