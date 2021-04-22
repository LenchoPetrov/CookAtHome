using CookAtHome.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookAtHome.Services.Interfaces
{
    public interface IRecipeTagService
    {
        void AddRecipeTags(HashSet<Tag> list, Recipe recipe);
    }
}
