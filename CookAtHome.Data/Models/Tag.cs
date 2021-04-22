using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CookAtHome.Data.Models
{
    public class Tag
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string Title { get; set; }

        public virtual ICollection<RecipeTag> Recipes { get; set; }
    }
}
