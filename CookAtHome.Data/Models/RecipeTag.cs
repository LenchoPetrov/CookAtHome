using CookAtHome.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookAtHome.Data.Models
{
    public class RecipeTag
    {
        public int TagId { get; set; }

        public Tag Tag { get; set; }

        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; }
    }
}
