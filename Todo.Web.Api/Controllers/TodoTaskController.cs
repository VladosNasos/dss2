using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Todo.Domain.Services;

namespace Todo.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoTaskController : ControllerBase
    {
        private readonly ITodoTaskService _service;

        public TodoTaskController(ITodoTaskService service)
        {
            _service = service;
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute, Required] int id)
        {
            var task = _service.GetTodoList(id);
            return task is null ? NotFound() : Ok(task);
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetTodoLists());

        [HttpPost]
        public IActionResult Create([FromBody] CreateTodoTaskInput dto)
        {
            if (dto.HolderId is null || dto.OwnerId is null
                || string.IsNullOrWhiteSpace(dto.Description))
                return BadRequest("OwnerId, HolderId and Description are required.");

            _service.Create(dto.OwnerId, dto.HolderId, dto.Description, dto.DueDate);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateTodoTaskInput dto)
        {
            _service.Update(id, dto.Description, dto.IsCompleted, dto.DueDate);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return NoContent();
        }

        public sealed record CreateTodoTaskInput(
            int? OwnerId,
            int? HolderId,
            string Description,
            DateTime DueDate);

        public sealed record UpdateTodoTaskInput(
            string Description,
            bool IsCompleted,
            DateTime DueDate);
    }
}
