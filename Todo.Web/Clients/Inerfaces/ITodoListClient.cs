using Refit;
using System.ComponentModel.DataAnnotations;
using Todo.Web.Clients.Models;

namespace Todo.Web.Clients.Inerfaces
{
    public interface ITodoListClient
    {
        [Get("")]
        Task<TodoListModel[]> GetAll();

        [Get("/{id}")]
        Task<TodoListModel?> GetById([Required] int id);

        [Post("")]
        Task Create([Body] CreateTodoListInputModel dto);

        [Put("/{id}")]
        Task Update(int id, [Body] UpdateTodoListInputModel dto);

        [Delete("/{id}")]
        Task Delete(int id);
    }
}
