using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sparta_Global_Profile.Models
{
    public class Hobby
    {
        public int HobbyId { get; set; }
        public string HobbyName { get; set; }
        public string HobbyDescription { get; set; }
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
    }
}
