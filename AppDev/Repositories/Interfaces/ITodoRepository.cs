using AppDev.DTOs.Requests;
using AppDev.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace AppDev.Repositories.Interfaces
{
  public interface ITodoRepository
  {
    IEnumerable<Todo> GetAll();
    IEnumerable<Todo> GetAll(string userId);
    IEnumerable<Todo> GetAll(string userId, string categoryName);
    IEnumerable<Todo> GetAllByCategoryId(int id);

    Task<bool> CreateTodo(CreateTodoRequest model, string userId);
    bool DeleteTodo(int id, string userId);
    Todo GetTodo(int id);
    Todo GetTodo(int id, string userId);
    bool EditTodo(EditTodoRequest model, string userId);


  }
}
