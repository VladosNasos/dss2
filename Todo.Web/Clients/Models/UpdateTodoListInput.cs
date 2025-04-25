using System.ComponentModel.DataAnnotations;

namespace Todo.Web.Api.Models
{
    public class UpdateTodoListInput
    {
        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public bool IsActive { get; set; }
    }
}
