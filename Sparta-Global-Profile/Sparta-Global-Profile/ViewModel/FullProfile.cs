using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sparta_Global_Profile.Models;

namespace Sparta_Global_Profile.ViewModel
{
    public class FullProfile
    {
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Profile> Profiles { get; set; }

        
    }
}
