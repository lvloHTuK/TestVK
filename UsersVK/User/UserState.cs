using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersVK.Models
{
    public enum StateEnum
    {
        Active,
        Blocked
    }
    public class UserState
    {
        public Guid Id { get; set; }
        public StateEnum Code { get; set; }
        public string Description { get; set; }
    }
}
