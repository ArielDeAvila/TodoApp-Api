using Todo.Server.Data.Entities;

namespace Todo.Server.DTO
{
    public class TaskResponseDto
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsCanceled { get; set; }
        public DateTime? CreatedAt { get; set; }

        public static explicit operator TaskResponseDto(NoteTask task)
        {
            return new TaskResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                IsCanceled = task.IsCanceled,
                CreatedAt = task.CreatedAt
            };
        }
    }
}
