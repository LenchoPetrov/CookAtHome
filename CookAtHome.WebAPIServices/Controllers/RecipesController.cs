using CookAtHome.Common;
using CookAtHome.Services.Interfaces;
using CookAtHome.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace CookAtHome.WebAPIServices.Controllers
{
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService recipes;

        public RecipesController(IRecipeService recipes)
        {
            this.recipes = recipes;
        }

        /// <summary>
        /// This methods return version of Recipe controller!
        /// </summary>
        [HttpGet, Route("api/recipes/version")]
        public IActionResult Version()
        {
            return Ok("Recipes version 1.0");
        }

        /// <summary>
        /// This method return recipe info!
        /// </summary>
        /// <remarks>Search for recipe by id</remarks>
        /// <response code="200">Found recipe!</response>
        /// <response code="404">Can't find recipe!</response>
        [HttpGet, Route("api/recipes/{id}")]
        public IActionResult Index(int id)
        {
            try
            {
                var recipe = recipes.GetRecipeInfoApi(id);
                return Ok(recipe);
            }
            catch (System.Exception)
            {
                return StatusCode(404, "Can't find recipe!");
            }
        }

        /// <summary>
        /// This method return recipes by category!
        /// </summary>
        /// <remarks>Search recipes by category</remarks>
        /// <response code="200">Found recipes!</response>
        /// <response code="400">Can't find recipes!</response>
        /// <response code="500">Oops! Can't process your search right now.</response>
        [HttpGet, Route("api/recipes/category/{category}")]
        public IActionResult Category(string category)
        {
            try
            {
                var recipesByCategory = recipes.GetAllRecipesShortInCategory(category);
                if (recipesByCategory.Count > 0)
                    return Ok(recipesByCategory);
                return StatusCode(404, "No recipes found!");
            }
            catch (System.Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// This method return recipes by user!
        /// </summary>
        /// <remarks>Search recipes by user</remarks>
        /// <response code="200">Found recipes!</response>
        /// <response code="400">Can't find recipes!</response>
        /// <response code="500">Oops! Can't process your search right now.</response>
        [Authorize]
        [HttpGet, Route("api/recipes/myrecipes/{username}")]
        public IActionResult MyRecipes(string username)
        {
            try
            {
                var recipesByUser = recipes.GetAllRecipesShortByUser(username);
                if (recipesByUser.Count > 0)
                    return Ok(recipesByUser);
                return StatusCode(404, "No recipes found!");
            }
            catch (System.Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// This method create new recipe!
        /// </summary>
        /// <remarks>Add recipe</remarks>
        /// <response code="201">Recipe created!</response>
        /// <response code="400">Recipe has missing/invalid values!</response>
        /// <response code="500">Oops! Can't create your recipe right now.</response>
        [HttpPost, Route("api/recipes/create")]
        public IActionResult Create(RecipeCreateShort model)
        {
            var fileContents = model.Photo.ToByteArray();
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                return StatusCode(201, "Recipe is created with id: " + recipes.CreateRecipe(model, fileContents, userId));
            }
            catch (System.Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// This method edit recipe!
        /// </summary>
        /// <remarks>Edit recipe</remarks>
        /// <response code="200">Edit successful!</response>
        /// <response code="400">Recipe has missing/invalid values!</response>
        /// <response code="500">Oops! Can't edit your recipe right now.</response>
        [HttpPost, Route("api/recipes/edit/{id}")]
        public IActionResult Edit(int id, [FromBody] RecipeEdit model)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var result = 0;
                if (recipes.UserIsOwner(id, userId))
                {
                    result = recipes.EditRecipe(model, userId, id);
                    if (result == 1)
                        return Ok("Recipe is edited!");
                    return StatusCode(400, "Recipe has missing/invalid values!");
                }
                return StatusCode(401, "Invalid credentials!");

            }
            catch (System.Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// This method delete recipe!
        /// </summary>
        /// <remarks>Delete recipe</remarks>
        /// <response code="200">Recipe deleted!</response>
        /// <response code="500">Oops! Can't delete your recipe right now.</response>
        [HttpDelete, Route("api/recipes/delete/{id}")]
        public IActionResult Destroy(int id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = 0;
            if (recipes.UserIsOwner(id, userId))
                result = recipes.DeleteRecipe(id);
            if (result == 1)
                return Ok(result);

            return StatusCode(500, "Can't delete your recipe right now!");
        }
    }
}