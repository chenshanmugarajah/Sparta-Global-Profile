using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sparta_Global_Profile.Models
{
    public class Skill
    {
        public int SkillId { get; set; }
        public string SkillName { get; set; }
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
    }
}
