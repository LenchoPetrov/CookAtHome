using CookAtHome.Data.Data;
using CookAtHome.Data.Models;
using CookAtHome.Services.Interfaces;
using CookAtHome.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CookAtHome.Services.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly CookAtHomeDbContext db;

        private readonly IRecipeService recipes;

        private readonly IUserService users;

        public CommentService(CookAtHomeDbContext db, IRecipeService recipes, IUserService users)
        {
            this.db = db;
            this.users = users;
            this.recipes = recipes;
        }

        public int CreateComment(int recipeId, string commentContent, string id)
        {
            try
            {
                var user = this.db.Users.FirstOrDefault(u => u.Id == id);
                var recipe = this.db.Recipes.FirstOrDefault(r => r.Id == recipeId);

                var comment = new Comment
                {
                    OwnerId = user.Id,
                    Owner = user,
                    Content = commentContent,
                    Recipe = recipe,
                    RecipeId = recipeId,
                    PublishDate = DateTime.UtcNow
                };

                this.db.Add(comment);
                this.db.SaveChanges();

                user.Comments.Add(comment);
                recipe.Comments.Add(comment);

                this.db.Update(user);
                this.db.Update(recipe);
                this.db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int DeleteComment(int commentId)
        {
            try
            {
                var comment = this.db.Comments.FirstOrDefault(u => u.Id == commentId);

                this.db.Remove(comment);
                this.db.SaveChanges();

                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public bool CheckCommentDeletePermision(string userId, int commentId)
        {
            try
            {
                var comment = this.db.Comments.First(c => c.Id == commentId);
                var recipe = this.db.Recipes.First(r => r.Id == comment.RecipeId);
                if (comment.OwnerId == userId || recipe.OwnerId == userId)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public HashSet<CommentByUser> GetCommentsByUser(string username)
        {
            var userId = this.db.Users.FirstOrDefault(u => u.Email == username).Id;
            var comments = this.db.Comments.Where(u => u.OwnerId == userId).Select(c => new CommentByUser
            {
                Id = c.Id,
                Content = c.Content,
                Owner = username,
                PublishDate = c.PublishDate,
                RecipeId = c.RecipeId,
                RecipeTitle = c.Recipe.Title
            }).ToHashSet();

            return comments;
        }
    }
}
