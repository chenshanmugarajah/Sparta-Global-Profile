using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sparta_Global_Profile.Models
{
    public class Course // c# , java , sdet
    {
        public Course()
        {
            Profiles = new HashSet<Profile>();
        }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string AcademyExperience { get; set; }
        public ICollection<Profile> Profiles { get; set; }
    }

    // class stream { } eng66 data14 sdet12
}
