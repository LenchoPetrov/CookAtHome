using CookAtHome.Data.Models;
using CookAtHome.Data.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookAtHome.Services.Models
{
    public class IndexRecipe
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Photo { get; set; }

        public byte[] PhotoBytes { get; set; }

        public string Content { get; set; }

        public int Time { get; set; }

        public CategoriesEnum Category { get; set; }

        public LevelsEnum Level { get; set; }

        public HashSet<Tag> Tags { get; set; }

        public string OwnerId { get; set; }

        public string OwnerEmail { get; set; }

        public DateTime PublishDate { get; set; }

        public HashSet<CommentIndex> Comments { get; set; } = new HashSet<CommentIndex>();
    }
}