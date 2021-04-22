using CookAtHome.Data.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CookAtHome.Data.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        [Required]
        public byte[] Picture { get; set; }

        [Required]
        [MinLength(3)]
        public string Title { get; set; }

        [Required]
        [MinLength(100)]
        public string Content { get; set; }

        public int Time { get; set; }

        public CategoriesEnum Category { get; set; }

        public LevelsEnum Level { get; set; }

        public DateTime PublishDate { get; set; }

        public string OwnerId { get; set; }

        public virtual User Owner { get; set; }

        public virtual ICollection<RecipeTag> Tags { get; set; }
        
        public virtual ICollection<Comment> Comments { get; set; }
    }
}