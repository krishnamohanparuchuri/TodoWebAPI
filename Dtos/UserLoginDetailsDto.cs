using System.ComponentModel.DataAnnotations;

namespace TodoWebAPI.Dtos
{
    public class UserLoginDetailsDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
