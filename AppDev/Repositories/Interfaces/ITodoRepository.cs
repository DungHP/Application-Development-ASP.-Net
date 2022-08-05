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
    Task<bool> CreateTodoWithUserId(TodoCategoriesViewModel viewModel, string userId);
    bool CreateTodo(TodoCategoriesViewModel viewModel);
    bool DeleteById(int id); 
    Todo GetById(int id);
    Todo GetByTodoIdAndUserId(int id, string userId);
    bool DeleteByIdAndUserId(int id, string userId);
    bool EditTodo(TodoCategoriesViewModel viewModel, string userId);
    IEnumerable<Todo> GetTodesByCategoryId(int id);



  }
}
