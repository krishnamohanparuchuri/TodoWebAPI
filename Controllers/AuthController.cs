using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using TodoWebAPI.Dtos;
using TodoWebAPI.HelperMethods;
using TodoWebAPI.Models;

namespace TodoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IPassword _password;
        private readonly IJsonwebToken _jsonwebToken;
        public static User user = new User();

        public AuthController(IPassword password,IJsonwebToken jsonwebToken)
        {
            _password = password;
            _jsonwebToken = jsonwebToken;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] UserRegisterDetailsDto userDetails)
        {
            _password.CreatePasswordHash(userDetails.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.UserName = userDetails.UserName;
            user.Email = userDetails.Email;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] UserLoginDetailsDto userDetails)
        {
            if(user.UserName != userDetails.UserName)
            {
                return BadRequest("User not found.");
            }

            if(!_password.VerifyPasswordHash(userDetails.Password, user.PasswordHash,user.PasswordSalt))
            {
                return BadRequest("You entered wrong password");
            }

            string token = _jsonwebToken.GenerateToken(user);
            return Ok(token);
        }

    }
}
