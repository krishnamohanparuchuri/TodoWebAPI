using System.ComponentModel.DataAnnotations;

namespace TodoWebAPI.Models
{
    public class User
    {
        
        public int ID { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Role Role { get; set; }

        //public string PasswordHas { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public List<Todo> Todos { get; set; }

    }
}
