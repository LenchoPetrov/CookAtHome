using CookAtHome.Data.Models;
using CookAtHome.Services.Interfaces;
using CookAtHome.Services.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CookAtHome.WebAPIServices.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// This methods return version of Users controller!
        /// </summary>
        [HttpGet, Route("api/user/version")]
        public IActionResult Version()
        {
            return Ok("Users version 1.0");
        }

        /// <summary>
        /// This methods return current user!
        /// </summary>
        /// <response code="200">There is login user!</response>
        /// <response code="404">No user logged in!</response>
        [HttpGet, Route("api/user/getcurrent")]
        public IActionResult GetCurrent()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
                return Ok("Current login user is: " + this.User.Identity.Name);

            return StatusCode(404, "No user logged in!");
        }

        /// <summary>
        /// This method is for login!
        /// </summary>
        /// <remarks>Login</remarks>
        /// <response code="200">Login successful!</response>
        /// <response code="400">Invalid syntax!</response>
        /// <response code="401">Invalid login attempt!</response>
        [HttpPost, Route("api/user/login")]
        public async Task<IActionResult> Login(LoginUser user)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);

                if (result.Succeeded)
                {
                    return Ok("Login is successful! User: " + user.Email);
                }
                return StatusCode(401, "Invalid login attempt!");
            }
            return StatusCode(400, "Invalid syntax!");
        }

        /// <summary>
        /// This method is for logout!
        /// </summary>
        /// <response code="200">Logout successful!</response>
        /// <response code="500">No user logged in yet!</response>
        [HttpPost, Route("api/user/logout")]
        public async Task<IActionResult> Logout()
        {
            var name = this.User.Identity.Name;
            if (name != null)
            {
                await _signInManager.SignOutAsync();
                return Ok(name + " logout successful!");
            }
            return StatusCode(500, "No user logged in yet!");
        }

        /// <summary>
        /// This method is for registration!
        /// </summary>
        /// <remarks>Register user</remarks>
        /// <response code="200">Registation is successful!</response>
        /// <response code="400">Invalid syntax!</response>
        /// <response code="500">Invalid registration attempt!</response>
        [HttpPost, Route("api/user/register")]
        public async Task<IActionResult> Register(RegisterUser model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Ok("User: " + model.Email + " is registered!");
                }
                return StatusCode(400, "Invalid syntax");
            }
            return StatusCode(500, "Invalid registration attempt");
        }
    }
}