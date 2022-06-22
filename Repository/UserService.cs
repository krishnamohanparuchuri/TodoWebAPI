using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TodoWebAPI.Authorization;
using TodoWebAPI.Data;
using TodoWebAPI.Dtos;
using TodoWebAPI.HelperMethods;
using TodoWebAPI.Models;

namespace TodoWebAPI.Repository
{
    public interface IUserService
    {
        Task<UserResponseDto> Authenticate(UserLoginDetailsDto model);
        IEnumerable<User> GetAll();

        Task<User> CreateUser(UserRegisterDetailsDto model);
        Task<User> GetById(int id);
    }
    public class UserService : IUserService
    {
        private TodoApiContext _context;
        private IJwtUtils _jwtUtils;
        private readonly JwtConfig _appSettings;
        private readonly IPassword _password;


        public UserService(
            TodoApiContext context,
            IJwtUtils jwtUtils,
            IOptions<JwtConfig> appSettings,IPassword password)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
            _password = password;
        }


        public async Task<User> CreateUser(UserRegisterDetailsDto model)
        {
            _password.CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);
           
            var user = new User()
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role= Role.User,
            };
            _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }
        public async Task<UserResponseDto> Authenticate(UserLoginDetailsDto model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == model.UserName);

            // validate
            if (user == null)
                throw new AppException("Username is incorrect");

            if (_password.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
            {
                var jwtToken = _jwtUtils.GenerateJwtToken(user);

                return new UserResponseDto(user, jwtToken);
            }
            else
            {
                throw new AppException("Password is incorrect");
            }

            // authentication successful so generate jwt token
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.Include(user => user.Todos);
        }

        public async Task<User> GetById(int id)
        {
            var user = await _context
                             .Users
                             .Include(user => user.Todos)
                             .FirstOrDefaultAsync(u => u.ID == id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }

    }
}
