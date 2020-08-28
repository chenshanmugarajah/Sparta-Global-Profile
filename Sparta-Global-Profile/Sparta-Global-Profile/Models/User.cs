using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sparta_Global_Profile.Models
{
    public class User
    {
        public int UserId { get; set; }
        public int? ProfileId { get; set; } //nullable
        public Profile Profile { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public int UserTypeId { get; set; }
        public UserType UserType { get; set; }
    }
}
