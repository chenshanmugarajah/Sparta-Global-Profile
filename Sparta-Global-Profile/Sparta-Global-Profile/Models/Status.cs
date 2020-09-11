using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sparta_Global_Profile.Models
{
    public class Status
    {
        public Status()
        {
            Profiles = new HashSet<Profile>();
        }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public ICollection<Profile> Profiles { get; set; }
    }
}
