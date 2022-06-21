namespace TodoWebAPI.Dtos
{
    public class TodoResponseDto
    {
        public Guid ID { get; set; }
        
        public string Title { get; set; }
       
        public string Description { get; set; }

        public bool Done { get; set; } = false;

        public DateTime Created { get; set; } = DateTime.Now;
    }
}
