using System.ComponentModel.DataAnnotations;
using TodoWebAPI.Models;

namespace TodoWebAPI.Dtos
{
    public class UserRegisterDetailsDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public Role Role { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
