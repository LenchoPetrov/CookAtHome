using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CookAtHome.Data.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(600)]
        public string Content { get; set; }

        [Required]
        public DateTime PublishDate { get; set; }

        public string OwnerId { get; set; }

        public virtual User Owner { get; set; }

        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; }
    }
}
