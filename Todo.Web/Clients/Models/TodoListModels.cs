namespace Todo.Web.Clients.Models
{
    public class TodoListModel
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime Date { get; set; }
        public int NumberOfTasks { get; set; }
    }

    public class CreateTodoListInputModel
    {
        public int OwnerId { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    public class UpdateTodoListInputModel
    {
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
