using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoWebAPI.Data;
using TodoWebAPI.Models;

namespace TodoWebAPI.Repository
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoApiContext _context;

        public TodoRepository(TodoApiContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Todo>>> GetAll() 
        {
            return await _context.Todos.ToListAsync();
        }

        public async Task<ActionResult<Todo>> Create(Todo todo)
        {
            if (todo == null)
            {
                throw new ArgumentNullException(nameof(todo));
            }

            await _context.Todos.AddAsync(todo);
            await _context.SaveChangesAsync();
            return todo;
        }

        public async Task<Todo> GetById(Guid id)
        {
            var todo = await _context.Todos.FindAsync(id);

            return todo;
           
        }

        public async Task Update(Todo todo)
        {
            var existItem = await _context.Todos.FirstOrDefaultAsync(p => p.ID == todo.ID);
            existItem.Title = todo.Title;
            existItem.Description = todo.Description;
            existItem.Done = todo.Done;
            _context.Todos.Update(existItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteById(Guid id)
        {
            var existItem = await _context.Todos.FirstOrDefaultAsync(p => p.ID == id);
            _context.Todos.Remove(existItem);
            await _context.SaveChangesAsync();
        }
     }
}
