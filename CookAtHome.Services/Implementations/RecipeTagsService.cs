using CookAtHome.Data.Data;
using CookAtHome.Data.Models;
using CookAtHome.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookAtHome.Services.Implementations
{
    public class RecipeTagsService : IRecipeTagService
    {
        private readonly CookAtHomeDbContext db;

        public RecipeTagsService(CookAtHomeDbContext db)
        {
            this.db = db;
        }

        public void AddRecipeTags(HashSet<Tag> list, Recipe recipe)
        {
            var listRecipeTags = new HashSet<RecipeTag>();
            foreach (var tag in list)
            {
                var recipeTag = new RecipeTag()
                {
                    TagId = tag.Id,
                    Tag = tag,
                    RecipeId = recipe.Id,
                    Recipe = recipe
                };
                this.db.RecipeTags.Add(recipeTag);
                this.db.SaveChanges();
                listRecipeTags.Add(recipeTag);
                tag.Recipes.Add(recipeTag);
                this.db.SaveChanges();
            }
            recipe.Tags = listRecipeTags;
            this.db.SaveChanges();
        }
    }
}
