using CookAtHome.Data.Models;
using CookAtHome.Services.Models;
using System.Collections.Generic;

namespace CookAtHome.Services.Interfaces
{
    public interface IRecipeService
    {
        //new methods
        int EditRecipe(RecipeEdit model, string userId, int recipeId);
        
        int CreateRecipe(RecipeCreateShort model, byte[] fileContents, string id);

        HashSet<RecipeInfoShort> GetAllRecipesShortByUser(string username);

        HashSet<RecipeInfoShort> GetAllRecipesShortInCategory(string category);

        RecipeInfo GetRecipeInfoApi(int id);
        //

        int CreateRecipe(CreateRecipe model, byte[] fileContents, string id);

        int EditRecipe(EditRecipe model, string userId);

        int DeleteRecipe(int id);

        IndexRecipe GetRecipe(int id);

        bool UserIsOwner(int id, string userId);
        
        EditRecipe GetRecipeInfo(int id);

        DeleteRecipe DeleteRecipeInfo(int id);

        HashSet<Recipe> GetAllRecipesInCategory(string category);

        HashSet<Recipe> GetAllRecipesByUser(string username);
    }
}
