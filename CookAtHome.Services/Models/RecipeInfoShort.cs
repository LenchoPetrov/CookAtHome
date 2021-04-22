using CookAtHome.Data.Models;
using CookAtHome.Data.Models.Enum;
using CookAtHome.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookAtHome.Services.Models
{
    public class RecipeInfoShort
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int Time { get; set; }

        public string Category { get; set; }

        public string Level { get; set; }

        public string OwnerEmail { get; set; }

        public DateTime PublishDate { get; set; }
    }
}