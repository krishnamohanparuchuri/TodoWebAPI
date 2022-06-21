using System.ComponentModel.DataAnnotations;

namespace TodoWebAPI.Models
{
    public class Todo
    {
        public Guid ID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public bool Done { get; set; } = false;

        public DateTime Created { get; set; } = DateTime.Now;

        public virtual int UserId { get; set; }
    }
}
