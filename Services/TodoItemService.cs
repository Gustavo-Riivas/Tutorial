using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTodo.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ApplicationDbContext _context;

        public TodoItemService(ApplicationDbContext context)
        {
            _context = context;
        }
public async Task<IActionResult> AddItem(NewTodoItem newItem)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    var currentUser = await _userManager.GetUserAsync(User);
    if (currentUser == null) return Unauthorized();

    var successful = await _todoItemService.AddItemAsync(newItem, currentUser);
    if (!successful)
    {
        return BadRequest(new { error = "Could not add item." });
    }

    return Ok();
}

public async Task<IActionResult> MarkDone(Guid id)
{
    if (id == Guid.Empty) return BadRequest();

    var currentUser = await _userManager.GetUserAsync(User);
    if (currentUser == null) return Unauthorized();

    var successful = await _todoItemService.MarkDoneAsync(id, currentUser);
    if (!successful) return BadRequest();

    return Ok();
}
public async Task<bool> AddItemAsync(NewTodoItem newItem, ApplicationUser user)
{
    var entity = new TodoItem
    {
        Id = Guid.NewGuid(),
        OwnerId = user.Id,
        IsDone = false,
        Title = newItem.Title,
        DueAt = DateTimeOffset.Now.AddDays(3)
    };

    // ...
}
public async Task<bool> MarkDoneAsync(Guid id, ApplicationUser user)
{
    var item = await _context.Items
        .Where(x => x.Id == id && x.OwnerId == user.Id)
        .SingleOrDefaultAsync();

    // ...
}
}