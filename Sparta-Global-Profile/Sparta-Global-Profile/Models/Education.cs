using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sparta_Global_Profile.Models
{
    public class Education
    {
        public Education()
        {
            Modules = new HashSet<Module>();
        }
        public int EducationId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        public string Establishment { get; set; }
        public string Qualification { get; set; }
        public string Grade { get; set; }
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
        public ICollection<Module> Modules { get; set; }
    }
}
