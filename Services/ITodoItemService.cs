using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;

namespace AspNetCoreTodo.Services
{
public interface ITodoItemService
{
Task<bool> AddItemAsync(NewTodoItem newItem, ApplicationUser user);

Task<bool> MarkDoneAsync(Guid id, ApplicationUser user);
}