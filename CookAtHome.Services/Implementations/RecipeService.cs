using CookAtHome.Data.Data;
using CookAtHome.Data.Models;
using CookAtHome.Data.Models.Enum;
using CookAtHome.Services.Interfaces;
using CookAtHome.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CookAtHome.Services.Implementations
{
    public class RecipeService : IRecipeService
    {
        private readonly CookAtHomeDbContext db;

        private readonly ITagService tags;

        private readonly IRecipeTagService recipeTags;

        public RecipeService(CookAtHomeDbContext db, ITagService tags, IRecipeTagService recipeTags)
        {
            this.db = db;
            this.tags = tags;
            this.recipeTags = recipeTags;
        }

        public int CreateRecipe(CreateRecipe model, byte[] fileContents, string id)
        {
            try
            {
                var user = this.db.Users.FirstOrDefault(u => u.Id == id);
                HashSet<string> tags = new HashSet<string>();
                tags.Add(model.Tag1);
                tags.Add(model.Tag2);
                tags.Add(model.Tag3);
                var list = this.tags.CheckTags(tags);

                var recipe = new Recipe
                {
                    Title = model.Title,
                    Picture = fileContents,
                    Content = model.Content,
                    Time = model.Time,
                    Category = model.Category,
                    Level = model.Level,
                    PublishDate = DateTime.UtcNow,
                    Owner = user,
                    OwnerId = user.Id
                };

                this.db.Add(recipe);
                this.db.SaveChanges();

                if (list.Count > 0)
                {
                    this.recipeTags.AddRecipeTags(list, recipe);
                }

                return recipe.Id;
            }
            catch (Exception)
            {

                return -1;
            }
        }

        public int EditRecipe(EditRecipe model, string userId)
        {
            var user = this.db.Users.FirstOrDefault(u => u.Id == userId);
            var recipe = this.db.Recipes.FirstOrDefault(r => r.Id == model.Id);
            recipe.Content = model.Content;
            recipe.Time = model.Time;
            recipe.Title = model.Title;

            this.db.Update(recipe);
            this.db.SaveChanges();

            return 1;
        }

        public int DeleteRecipe(int id)
        {
            try
            {
                var recipe = this.db.Recipes.FirstOrDefault(u => u.Id == id);
                var recipeTags = this.db.RecipeTags.Where(r => r.RecipeId == id).ToList();
                if (recipeTags.Count > 0)
                {
                    foreach (var rectag in recipeTags)
                    {
                        this.db.RecipeTags.Remove(rectag);
                    }
                }
                this.db.SaveChanges();
                this.db.Remove(recipe);
                this.db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public IndexRecipe GetRecipe(int id)
        {
            var recipe = this.db.Recipes.FirstOrDefault(u => u.Id == id);
            var tags = this.db.RecipeTags.Where(r => r.RecipeId == id).ToList();
            var user = this.db.Users.FirstOrDefault(u => u.Id == recipe.OwnerId);
            var commentsList = this.db.Comments.Where(r => r.RecipeId == id).Select(c => new CommentIndex
            {
                Id = c.Id,
                Content = c.Content,
                PublishDate = c.PublishDate,
                RecipeId = c.RecipeId,
                UserId = c.OwnerId,
                Username = c.Owner.UserName,
                RecipeOwnerName = recipe.Owner.UserName
            }).ToHashSet();
            HashSet<Tag> tagsList = new HashSet<Tag>();

            foreach (var tag in tags)
            {
                tagsList.Add(this.db.Tags.FirstOrDefault(t => t.Id == tag.TagId));
            }

            var recipeIndex = new IndexRecipe
            {
                Id = recipe.Id,
                Title = recipe.Title,
                Content = recipe.Content,
                Category = recipe.Category,
                Level = recipe.Level,
                OwnerId = recipe.OwnerId,
                Photo = recipe.Picture == null ? null : String.Format("data:image/gif;base64,{0}", Convert.ToBase64String(recipe.Picture)),
                PhotoBytes = recipe.Picture,
                Tags = tagsList,
                Time = recipe.Time,
                PublishDate = recipe.PublishDate,
                OwnerEmail = user.Email,
                Comments = commentsList
            };

            return recipeIndex;
        }

        public bool UserIsOwner(int id, string userId)
        {
            var ownerId = this.db.Recipes.FirstOrDefault(u => u.Id == id).OwnerId;
            if (ownerId == userId)
                return true;

            return false; ;
        }

        public EditRecipe GetRecipeInfo(int id)
        {
            var recipe = this.db.Recipes.FirstOrDefault(u => u.Id == id);
            return new EditRecipe
            {
                Id = id,
                Title = recipe.Title,
                Content = recipe.Content,
                Time = recipe.Time
            };
        }

        public DeleteRecipe DeleteRecipeInfo(int id)
        {
            var recipe = this.db.Recipes.FirstOrDefault(u => u.Id == id);
            return new DeleteRecipe
            {
                Id = id,
                Title = recipe.Title,
                Content = recipe.Content,
                Category = recipe.Category,
            };
        }

        public HashSet<Recipe> GetAllRecipesInCategory(string category)
        {
            Enum.TryParse(category, out CategoriesEnum cat);
            var recipes = this.db.Recipes.Where(u => u.Category == cat).ToHashSet();
            return recipes;
        }

        public HashSet<Recipe> GetAllRecipesByUser(string username)
        {
            var user = this.db.Users.FirstOrDefault(u => u.Email == username);
            var recipes = this.db.Recipes.Where(u => u.OwnerId == user.Id).ToHashSet();
            return recipes;
        }

        public int EditRecipe(RecipeEdit model, string userId, int recipeId)
        {
            try
            {
                var user = this.db.Users.FirstOrDefault(u => u.Id == userId);
                var recipe = this.db.Recipes.FirstOrDefault(r => r.Id == recipeId);
                recipe.Content = model.Content;
                recipe.Time = model.Time;
                recipe.Title = model.Title;

                this.db.Update(recipe);
                this.db.SaveChanges();

                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        // new
        public int CreateRecipe(RecipeCreateShort model, byte[] fileContents, string id)
        {
            try
            {
                var user = this.db.Users.FirstOrDefault(u => u.Id == id);
                HashSet<string> tags = new HashSet<string>();
                tags.Add(model.Tag1);
                tags.Add(model.Tag2);
                tags.Add(model.Tag3);
                var list = this.tags.CheckTags(tags);

                var recipe = new Recipe
                {
                    Title = model.Title,
                    Picture = fileContents,
                    Content = model.Content,
                    Time = model.Time,
                    Category = model.Category,
                    Level = model.Level,
                    PublishDate = DateTime.UtcNow,
                    Owner = user,
                    OwnerId = user.Id
                };

                this.db.Add(recipe);
                this.db.SaveChanges();

                if (list.Count > 0)
                {
                    this.recipeTags.AddRecipeTags(list, recipe);
                }

                return recipe.Id;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public HashSet<RecipeInfoShort> GetAllRecipesShortByUser(string username)
        {
            var user = this.db.Users.FirstOrDefault(u => u.Email == username);
            var recipes = this.db.Recipes.Where(u => u.OwnerId == user.Id).ToHashSet();
            var result = ConvertToRecipeShort(recipes);
            return result;
        }

        private HashSet<RecipeInfoShort> ConvertToRecipeShort(HashSet<Recipe> recipes)
        {
            HashSet<RecipeInfoShort> recipesList = new HashSet<RecipeInfoShort>();

            foreach (var r in recipes)
            {
                var recipe = new RecipeInfoShort
                {
                    Id = r.Id,
                    Title = r.Title,
                    Content = r.Content,
                    Level = r.Level.ToString(),
                    Category = r.Category.ToString(),
                    Time = r.Time,
                    OwnerEmail = r.Owner.Email,
                    PublishDate = r.PublishDate
                };
                recipesList.Add(recipe);
            }
            return recipesList;
        }

        public HashSet<RecipeInfoShort> GetAllRecipesShortInCategory(string category)
        {
            Enum.TryParse(category, out CategoriesEnum cat);
            var recipes = this.db.Recipes.Where(u => u.Category == cat).ToHashSet();
            var result = ConvertToRecipeShort(recipes);
            return result;
        }

        public RecipeInfo GetRecipeInfoApi(int id)
        {
            var recipe = this.db.Recipes.FirstOrDefault(u => u.Id == id);
            var tags = this.db.RecipeTags.Where(r => r.RecipeId == id).ToList();
            var user = this.db.Users.FirstOrDefault(u => u.Id == recipe.OwnerId);
            var commentsList = this.db.Comments.Where(r => r.RecipeId == id).Select(c => new CommentIndex
            {
                Id = c.Id,
                Content = c.Content,
                PublishDate = c.PublishDate,
                RecipeId = c.RecipeId,
                UserId = c.OwnerId,
                Username = c.Owner.UserName,
                RecipeOwnerName = recipe.Owner.UserName
            }).ToHashSet();

            HashSet<string> tagsList = new HashSet<string>();

            foreach (var tag in tags)
            {
                tagsList.Add(this.db.Tags.FirstOrDefault(t => t.Id == tag.TagId).Title);
            }

            var recipeConvert = new RecipeInfo
            {
                Id = recipe.Id,
                Title = recipe.Title,
                Content = recipe.Content,
                Level = recipe.Level.ToString(),
                Category = recipe.Category.ToString(),
                Time = recipe.Time,
                OwnerEmail = user.Email,
                PublishDate = recipe.PublishDate,
                Comments = commentsList,
                Tags = tagsList
            };
            return recipeConvert;
        }
    }
}