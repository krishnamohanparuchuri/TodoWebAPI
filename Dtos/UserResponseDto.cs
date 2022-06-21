using TodoWebAPI.Models;

namespace TodoWebAPI.Dtos
{
    public class UserResponseDto 
    {
        public int ID { get; set; }
        public string UserName { get; set; }

        public Role Role { get; set; }
        public string Token { get; set; }

        public UserResponseDto(User user, string token)
        {
            ID = user.ID;
            UserName = user.UserName;
            Role = user.Role;
            Token = token;
        }
    }
}
