using System;
using System.Collections.Generic;
using System.Text;

namespace CookAtHome.Services.Models
{
    public class CommentByUser
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        public string Owner { get; set; }

        public int RecipeId { get; set; }

        public string RecipeTitle { get; set; }
    }
}
