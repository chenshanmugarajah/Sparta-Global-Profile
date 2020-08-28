using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sparta_Global_Profile.Models
{
    public class ProjectLink
    {
        public int ProjectLinkId { get; set; }
        public int SpartaProjectId { get; set; }
        public SpartaProject SpartaProject { get; set; }
        public string LinkText { get; set; }
        public string Url { get; set; }
    }
}
