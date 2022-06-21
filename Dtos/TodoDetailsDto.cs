using System.ComponentModel.DataAnnotations;

namespace TodoWebAPI.Dtos
{
    public class TodoDetailsDto
    {
       

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int UserId { get; set; }

        
    }
}
