using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CookAtHome.Data.Models
{
    public class User : IdentityUser
    {
        public virtual ICollection<Recipe> Recipes { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}