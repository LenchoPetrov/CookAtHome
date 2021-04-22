using CookAtHome.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookAtHome.Services.Models
{
    public class CommentsAndSection
    {
        public IEnumerable<CommentIndex> Comments { get; set; }

        public int SectionNumber { get; set; }
    }
}
