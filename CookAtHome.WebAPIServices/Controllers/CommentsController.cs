using CookAtHome.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace CookAtHome.WebAPIServices.Controllers
{
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService comments;

        public CommentsController(ICommentService comments)
        {
            this.comments = comments;
        }

        /// <summary>
        /// This methods return version of Comment controller!
        /// </summary>
        [HttpGet, Route("api/coments/version")]
        public IActionResult Version()
        {
            return Ok("Comments version 1.0");
        }

        /// <summary>
        /// This method return all comments posted by searched user!
        /// </summary>
        /// <remarks>Create comment</remarks>
        /// <response code="200">Found comments!</response>
        /// <response code="404">Can't find comments!</response>
        /// <response code="500">Oops! Can't process your search right now.</response>
        [HttpGet, Route("api/comments/getcommentsbyuser/{username}")]
        public IActionResult GetAllCommentsByUser(string username)
        {
            try
            {
                var commentsList = comments.GetCommentsByUser(username);
                if (commentsList.Count > 0)
                    return Ok(commentsList);
                return StatusCode(404, "No comments found!");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// This method create comment!
        /// </summary>
        /// <remarks>Create comment</remarks>
        /// <response code="200">Comment created!</response>
        /// <response code="401">Comment has missing/invalid values!</response>
        /// <response code="500">Oops! Can't create your comment right now.</response>
        [HttpPost, Route("api/comments/create")]
        public IActionResult Create(int recipeId, string commentContent)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var comment = comments.CreateComment(recipeId, commentContent, userId);
            if (comment == 1)
                return StatusCode(201, "Comment created!");
            return StatusCode(500);
        }

        /// <summary>
        /// This method delete comment!
        /// </summary>
        /// <remarks>Delete comment</remarks>
        /// <response code="200">Comment deleted!</response>
        /// <response code="500">Oops! Can't delete comment right now.</response>
        [HttpDelete, Route("api/comments/delete/{id}")]
        public IActionResult Delete(int id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (comments.CheckCommentDeletePermision(userId, id))
            {
                var result = comments.DeleteComment(id);
                if (result==1)
                  return Ok("Comment is deleted!");
            }
            return StatusCode(500);
        }
    }
}