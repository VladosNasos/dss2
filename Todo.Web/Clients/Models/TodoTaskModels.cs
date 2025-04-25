namespace Todo.Web.Clients.Models
{
    public class TodoTaskModel
    {
        public int Id { get; set; }
        public int TodoId { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DueDate { get; set; }
    }

    public class CreateTodoTaskInputModel
    {
        public int OwnerId { get; set; }
        public int HolderId { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
    }

    public class UpdateTodoTaskInputModel
    {
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public DateTime DueDate { get; set; }
    }
}
