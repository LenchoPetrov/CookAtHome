using CookAtHome.Data.Models;
using CookAtHome.Services.Models;
using System;
using System.Collections.Generic;

namespace CookAtHome.Services.Models
{
    public class RecipeInfo
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int Time { get; set; }

        public string Category { get; set; }

        public string Level { get; set; }

        public string OwnerEmail { get; set; }

        public DateTime PublishDate { get; set; }

        public HashSet<string> Tags { get; set; }

        public HashSet<CommentIndex> Comments { get; set; } = new HashSet<CommentIndex>();
    }
}
