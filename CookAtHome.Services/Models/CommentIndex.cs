using System;
using System.Collections.Generic;
using System.Text;

namespace CookAtHome.Services.Models
{
    public class CommentIndex
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        public int RecipeId { get; set; }

        public string UserId { get; set; }

        public string Username { get; set; }

        public string RecipeOwnerName { get; set; }
    }
}
