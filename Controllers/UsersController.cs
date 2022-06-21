using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoWebAPI.Dtos;
using TodoWebAPI.Models;
using TodoWebAPI.Repository;

namespace TodoWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] UserRegisterDetailsDto userDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User user = await _userService.CreateUser(userDetails);

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Authenticate(UserLoginDetailsDto model)
        {
            var response = await _userService.Authenticate(model);
            return Ok(response);
        }

        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            // only admins can access other user records
            /*var currentUser = (User)HttpContext.Items["User"];
            if (id != currentUser.ID && currentUser.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });*/

            var user = await _userService.GetById(id);
            return Ok(user);
        }
    }
}
