using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CookAtHome.Services.Models
{
    public class CreateComment
    {
        [Required]
        [MinLength(1, ErrorMessage = "Content has to be at least 1 symbols.")]
        [MaxLength(600, ErrorMessage = "Content has to be maximum 600 symbols.")]
        public string Content { get; set; }

        public string OwnerId { get; set; }

        public int RecipeId { get; set; }
    }
}
