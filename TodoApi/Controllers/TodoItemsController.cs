namespace TodoApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using TodoApi.Data;
    using TodoApi.Models;

    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET: api/TodoItems
        /// Returns all todo items.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return await _context.TodoItems.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// GET: api/TodoItems/{id}
        /// Returns a single todo item by ID.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(int id)
        {
            var todoItem = await _context.TodoItems.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        /// <summary>
        /// POST: api/TodoItems
        /// Creates a new todo item.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<TodoItem>> CreateTodoItem([FromBody] TodoItem todoItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        }

        /// <summary>
        /// PUT: api/TodoItems/{id}
        /// Updates an existing todo item.
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateTodoItem(int id, [FromBody] TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest("ID in URL does not match ID in body.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existing = await _context.TodoItems.FindAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            // Update fields
            existing.Title = todoItem.Title;
            existing.Description = todoItem.Description;
            existing.IsCompleted = todoItem.IsCompleted;

            _context.Entry(existing).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// DELETE: api/TodoItems/{id}
        /// Deletes a todo item.
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
