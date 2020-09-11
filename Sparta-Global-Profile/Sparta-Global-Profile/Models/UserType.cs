using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Sparta_Global_Profile.Models
{
    public class UserType
    {
        public UserType()
        {
            Users = new HashSet<User>();
        }
        [DisplayName("User Role")]
        public int UserTypeId { get; set; }
        public string UserTypeName { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
