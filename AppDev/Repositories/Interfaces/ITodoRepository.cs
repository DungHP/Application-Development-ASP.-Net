using AppDev.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using WebApplication2.Models;

namespace AppDev.Repositories.Interfaces
{
  public interface ITodoRepository
  {
    IEnumerable<Todo> GetAll();
    bool CreateTodo(TodoCategoriesViewModel viewModel);
    bool DeleteById(int id); 
    Todo GetById(int id);
    Todo GetByTodoIdAndUserId(int id, string userId);
    bool DeleteByIdAndUserId(int id, string userId);
    bool EditTodo(TodoCategoriesViewModel viewModel);
    IEnumerable<Todo> GetTodesByCategoryId(int id);

  }
}
