using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sparta_Global_Profile.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int SectionIndex { get; set; }
        public string CommentContent { get; set; }
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
    }
}
