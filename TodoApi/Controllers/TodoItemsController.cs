using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataModel;
using TodoApi.Services;

namespace TodoApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoItemsService _todoItemsService;

        //private readonly TodoContext _context;

        public TodoItemsController(TodoItemsService todoItemsService)
        {
            _todoItemsService = todoItemsService;
        }

        // // GET: api/TodoItems
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        // {
        //   if (_context.TodoItems == null)
        //   {
        //       return NotFound();
        //   }
        //     return await _context.TodoItems.ToListAsync();
        // }

        // // GET: api/TodoItems/5
        // [HttpGet("{id}")]
        // public async Task<ActionResult<TodoItem>> GetTodoItem(int id)
        // {
        //   if (_context.TodoItems == null)
        //   {
        //       return NotFound();
        //   }
        //     var todoItem = await _context.TodoItems.FindAsync(id);

        //     if (todoItem == null)
        //     {
        //         return NotFound();
        //     }

        //     return todoItem;
        // }

        // // PUT: api/TodoItems/5
        // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutTodoItem(int id, TodoItem todoItem)
        // {
        //     if (id != todoItem.ID)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Entry(todoItem).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!TodoItemExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return NoContent();
        // }

        // // POST: api/TodoItems
        // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPost]
        // public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        // {
        //   if (_context.TodoItems == null)
        //   {
        //       return Problem("Entity set 'TodoContext.TodoItems'  is null.");
        //   }
        //     _context.TodoItems.Add(todoItem);
        //     await _context.SaveChangesAsync();

        //     return CreatedAtAction("GetTodoItem", new { id = todoItem.ID }, todoItem);
        // }

        // // DELETE: api/TodoItems/5
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteTodoItem(int id)
        // {
        //     if (_context.TodoItems == null)
        //     {
        //         return NotFound();
        //     }
        //     var todoItem = await _context.TodoItems.FindAsync(id);
        //     if (todoItem == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.TodoItems.Remove(todoItem);
        //     await _context.SaveChangesAsync();

        //     return NoContent();
        // }

        // private bool TodoItemExists(int id)
        // {
        //     return (_context.TodoItems?.Any(e => e.ID == id)).GetValueOrDefault();
        // }

        [HttpGet]
        public async Task<List<TodoItems>> Get() =>
       await _todoItemsService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<TodoItems>> Get(string id)
        {
            var book = await _todoItemsService.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpGet("{isCompleted}")]
        public async Task<List<TodoItems>> Get(bool isCompleted) =>
        await _todoItemsService.GetAsync(isCompleted);
        

        [HttpPost]
        public async Task<IActionResult> Post(TodoItems newItem)
        {
            await _todoItemsService.CreateAsync(newItem);

            return CreatedAtAction(nameof(Get), new { id = newItem.Id }, newItem);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, TodoItems updatedItem)
        {
            var todoItem = await _todoItemsService.GetAsync(id);

            if (todoItem is null)
            {
                return NotFound();
            }

            updatedItem.Id = todoItem.Id;

            await _todoItemsService.UpdateAsync(id, updatedItem);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var todoItem = await _todoItemsService.GetAsync(id);

            if (todoItem is null)
            {
                return NotFound();
            }

            await _todoItemsService.RemoveAsync(id);

            return NoContent();
        }
    }
}
