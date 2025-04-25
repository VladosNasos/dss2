using Refit;
using System.ComponentModel.DataAnnotations;
using Todo.Web.Clients.Models;

namespace Todo.Web.Clients.Inerfaces
{
    public interface ITodoTaskClient
    {
        [Get("")]
        Task<TodoTaskModel[]> GetAll();

        [Get("/{id}")]
        Task<TodoTaskModel?> GetById([Required] int id);

        [Post("")]
        Task Create([Body] CreateTodoTaskInputModel dto);

        [Put("/{id}")]
        Task Update(int id, [Body] UpdateTodoTaskInputModel dto);

        [Delete("/{id}")]
        Task Delete(int id);
    }
}
