
using Microsoft.AspNetCore.Mvc;
using TodoWebAPI.Dtos;
using TodoWebAPI.Models;
using TodoWebAPI.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoRepository _todoRepository;

        public TodosController(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }
        // GET: api/<TodosController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> GetAll()
        {
            return await _todoRepository.GetAll();
        }

        // GET api/<TodosController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> Get(Guid id)
        {
            var todo = await _todoRepository.GetById(id);
            if (todo == null) return NotFound();
            return Ok(todo);
        }

        // POST api/<TodosController>
        [HttpPost]
        public async Task<ActionResult<Todo>> Post([FromBody] TodoDetailsDto todoDetails)
        {
            var todo = new Todo()
            {
                Title = todoDetails.Title,
                Description = todoDetails.Description,
                UserId = todoDetails.UserId,
            };
            return await _todoRepository.Create(todo);
            //return CreatedAtAction("GetById", new { todo.ID }, todo);
        }

        // PUT api/<TodosController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Todo todo)
        {
            if (id != todo.ID)
                return BadRequest();

            var existingTodo = await _todoRepository.GetById(id);
            if (existingTodo == null) return NotFound();

            await _todoRepository.Update(todo);
            return NoContent();
        }

        // DELETE api/<TodosController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var existingTodo = await _todoRepository.GetById(id);
            if (existingTodo == null) return NotFound();
            await _todoRepository.DeleteById(id);
            return Ok(existingTodo);
        }
    }
}
