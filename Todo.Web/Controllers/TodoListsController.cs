using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Web.Clients.Inerfaces;
using Todo.Web.Clients.Models;

namespace Todo.Web.Controllers
{
    [Authorize]
    public class TodoListsController : Controller
    {
        private readonly ITodoListClient _lists;
        private readonly ITodoTaskClient _tasks;

        public TodoListsController(ITodoListClient lists, ITodoTaskClient tasks)
        {
            _lists = lists;
            _tasks = tasks;
        }

        /* -------- Index -------- */
        public async Task<IActionResult> Index()
        {
            var lists = await _lists.GetAll();
            var tasks = await _tasks.GetAll();
            foreach (var l in lists)
                l.NumberOfTasks = tasks.Count(t => t.TodoId == l.Id);

            return View(lists);
        }

        /* -------- Details (со вложенными задачами) -------- */
        public async Task<IActionResult> Details(int id)
        {
            var list = await _lists.GetById(id);
            if (list == null) return NotFound();

            var allTasks = await _tasks.GetAll();
            ViewBag.Tasks = allTasks.Where(t => t.TodoId == id).ToArray();

            return View(list);
        }

        /* -------- Create -------- */
        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTodoListInputModel dto)
        {
            if (!ModelState.IsValid) return View(dto);

            dto.OwnerId = int.Parse(User.FindFirst("userId")!.Value);
            await _lists.Create(dto);
            return RedirectToAction(nameof(Index));
        }

        /* -------- Edit -------- */
        public async Task<IActionResult> Edit(int id)
        {
            var list = await _lists.GetById(id);
            return list == null ? NotFound() : View(list);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateTodoListInputModel dto)
        {
            if (!ModelState.IsValid) return View(dto);

            await _lists.Update(id, dto);
            return RedirectToAction(nameof(Index));
        }

        /* -------- Delete -------- */
        public async Task<IActionResult> Delete(int id)
        {
            var list = await _lists.GetById(id);
            return list == null ? NotFound() : View(list);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _lists.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        /* -------- Inline create task (из Details) -------- */
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTask(int listId, string description, DateTime dueDate)
        {
            await _tasks.Create(new CreateTodoTaskInputModel
            {
                OwnerId = int.Parse(User.FindFirst("userId")!.Value),
                HolderId = listId,
                Description = description,
                DueDate = dueDate
            });
            return RedirectToAction(nameof(Details), new { id = listId });
        }
    }
}
