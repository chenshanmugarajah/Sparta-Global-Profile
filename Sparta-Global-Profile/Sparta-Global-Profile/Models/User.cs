using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sparta_Global_Profile.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }

        [DisplayName("Email")]
        [RegularExpression(@"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$", ErrorMessage = "Email not valid")]
        [Required(ErrorMessage="This field is required.")]
        public string UserEmail { get; set; }

        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,50}",ErrorMessage = "Password must be atleast 8 characters long and contain atleast an upper case letter, a lower case letter and a number")]
        [Required(ErrorMessage = "This field is required.")]
     
        public string UserPassword { get; set; }
        public int UserTypeId { get; set; }
        public UserType UserType { get; set; }
       
    }
}
