using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoWebAPI.Models;

namespace TodoWebAPI.Data
{
    
        public class TodoApiContext : IdentityDbContext
        {

            public TodoApiContext(DbContextOptions<TodoApiContext> options)
                : base(options)
            {
            }
            public virtual DbSet<Todo> Todos { get; set; }

            public virtual DbSet<User> Users { get; set; }
        }
    
}
