using CookAtHome.Data.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookAtHome.Services.Models
{
    public class DeleteRecipe
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public CategoriesEnum Category { get; set; }
    }
}
