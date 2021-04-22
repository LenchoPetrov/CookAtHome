using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CookAtHome.Services.Models
{
    public class RecipeEdit
    {
        public string Title { get; set; }

        [Required]
        [MinLength(100, ErrorMessage = "Content has to be at least 100 symbols.")]
        public string Content { get; set; }

        public int Time { get; set; }

    }
}
