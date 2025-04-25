using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Todo.Domain.Services;

namespace Todo.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoListController : ControllerBase
    {
        private readonly ITodoListService _service;

        public TodoListController(ITodoListService service)
        {
            _service = service;
        }

        // GET api/todolist/5
        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute, Required] int id)
        {
            var list = _service.GetTodoList(id);
            return list is null ? NotFound() : Ok(list);
        }

        // GET api/todolist
        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetTodoLists());

        // POST api/todolist
        [HttpPost]
        public IActionResult Create([FromBody] CreateTodoListInput dto)
        {
            if (dto.OwnerId is null || string.IsNullOrWhiteSpace(dto.Description))
                return BadRequest("OwnerId and Description are required.");

            _service.Create(dto.OwnerId, dto.Description);
            return Ok();
        }

        // PUT api/todolist/5
        [HttpPut("{id:int}")]
        public IActionResult Update(
            [FromRoute] int id,
            [FromBody] UpdateTodoListInput dto)
        {
            _service.Update(id, dto.Description, dto.IsActive);
            return NoContent();
        }

        // DELETE api/todolist/5
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return NoContent();
        }

        // DTO-классы
        public sealed record CreateTodoListInput(int? OwnerId, string Description);
        public sealed record UpdateTodoListInput(string Description, bool IsActive);
    }
}
