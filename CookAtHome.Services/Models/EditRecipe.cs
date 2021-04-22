using CookAtHome.Data.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CookAtHome.Services.Models
{
    public class EditRecipe
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [Required]
        [MinLength(100, ErrorMessage = "Title has to be at least 100 symbols.")]
        public string Content { get; set; }

        public int Time { get; set; }

    }
}
