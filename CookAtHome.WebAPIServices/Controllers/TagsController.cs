using CookAtHome.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CookAtHome.WebAPIServices.Controllers
{
    public class TagsController : ControllerBase
    {
        private readonly ITagService tags;

        public TagsController(ITagService tags)
        {
            this.tags = tags;
        }

        /// <summary>
        /// This methods return version of Tag controller!
        /// </summary>
        [HttpGet, Route("api/tags/version")]
        public IActionResult Version()
        {
            return Ok("Tags version 1.0");
        }

        /// <summary>
        /// This method return all tags!
        /// </summary>
        /// <remarks>Get all tags!</remarks>
        /// <response code="200">Found tags!</response>
        /// <response code="404">Can't find tags!</response>
        /// <response code="500">Oops! Can't process your search right now.</response>
        [HttpGet, Route("api/tags/all")]
        public IActionResult All()
        {
            try
            {
                var tagsList = tags.GetAll();
                if (tagsList == null)
                    return StatusCode(404, "Can't find tags!");
                return Ok(tagsList);
            }
            catch (System.Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// This method return tags that contain searched string!
        /// </summary>
        /// <remarks>Search tags!</remarks>
        /// <response code="200">Found tags!</response>
        /// <response code="404">Can't find tags!</response>
        /// <response code="500">Oops! Can't process your search right now.</response>
        [HttpGet, Route("api/tags/search/{text}")]
        public IActionResult Search(string text)
        {
            try
            {
                var tagsList = tags.GetSearchedTags(text);
                if (tagsList == null)
                    return StatusCode(404, "Can't find tags!"); ;
                return Ok(tagsList);
            }
            catch (System.Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}