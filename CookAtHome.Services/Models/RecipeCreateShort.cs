using CookAtHome.Data.Models;
using CookAtHome.Data.Models.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CookAtHome.Services.Models
{
    public class RecipeCreateShort
    {
        [Required]
        [MinLength(2, ErrorMessage = "Title has to be at least 2 symbols.")]
        [MaxLength(50, ErrorMessage = "Title has to be maximum 50 symbols.")]
        public string Title { get; set; }

        [Required]
        public IFormFile Photo { get; set; }

        [Required]
        [MinLength(100, ErrorMessage = "Content has to be at least 100 symbols.")]
        public string Content { get; set; }

        public int Time { get; set; }

        public CategoriesEnum Category { get; set; }

        public LevelsEnum Level { get; set; }

        [MinLength(2, ErrorMessage = "Tag1 has to be at least 2 symbols.")]
        public string Tag1 { get; set; }
        
        [MinLength(2, ErrorMessage = "Tag2 has to be at least 2 symbols.")]
        public string Tag2 { get; set; }
        
        [MinLength(2, ErrorMessage = "Tag3 has to be at least 2 symbols.")]
        public string Tag3 { get; set; }
    }
}
