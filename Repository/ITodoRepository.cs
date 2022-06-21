using Microsoft.AspNetCore.Mvc;
using TodoWebAPI.Models;

namespace TodoWebAPI.Repository
{
    public interface ITodoRepository
    {
        Task<ActionResult<IEnumerable<Todo>>> GetAll();
        Task<Todo> GetById(Guid id);

        Task<ActionResult<Todo>> Create(Todo todo);

        Task DeleteById(Guid id);

        Task Update(Todo todo);
    }
}
