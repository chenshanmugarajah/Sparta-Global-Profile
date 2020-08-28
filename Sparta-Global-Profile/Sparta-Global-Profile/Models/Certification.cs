using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sparta_Global_Profile.Models
{
    public class Certification
    {
        public int CertificationId { get; set; }
        public string CertificationName { get; set; }
        public string Summary { get; set; }
        public int ProfileId { get; set; } 
        public Profile Profile { get; set; }
    }
}
