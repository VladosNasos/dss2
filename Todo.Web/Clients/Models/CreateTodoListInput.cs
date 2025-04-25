using System.ComponentModel.DataAnnotations;

namespace Todo.Web.Api.Models
{
    public class CreateTodoListInput
    {
        [Required]
        public int? OwnerId { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;
    }
}
