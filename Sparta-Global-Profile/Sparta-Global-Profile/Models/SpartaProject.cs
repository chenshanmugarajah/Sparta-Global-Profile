using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sparta_Global_Profile.Models
{
    public class SpartaProject
    {
        public SpartaProject()
        {
            ProjectLinks = new HashSet<ProjectLink>();
        }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectBio { get; set; }
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
        public ICollection<ProjectLink> ProjectLinks { get; set; }
        
    }
}
