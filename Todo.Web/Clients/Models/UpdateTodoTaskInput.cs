using System;
using System.ComponentModel.DataAnnotations;

namespace Todo.Web.Api.Models
{
    public class UpdateTodoTaskInput
    {
        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public DateTime DueDate { get; set; }
    }
}
