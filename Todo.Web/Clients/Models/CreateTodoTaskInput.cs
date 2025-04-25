using System;
using System.ComponentModel.DataAnnotations;

namespace Todo.Web.Api.Models
{
    public class CreateTodoTaskInput
    {
        [Required]
        public int? OwnerId { get; set; }

        [Required]
        public int? HolderId { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public DateTime DueDate { get; set; }
    }
}
