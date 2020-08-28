using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sparta_Global_Profile.Models
{
    public class Module
    {
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public int CourseYear { get; set; }
        public int EducationId { get; set; } 
        public Education Education { get; set; }
        
    }
}
