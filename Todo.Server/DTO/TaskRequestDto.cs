using Todo.Server.Data.Entities;

namespace Todo.Server.DTO;

public class TaskRequestDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool? IsCompleted { get; set; }
    public string? CreatedAt { get; set; }
    public int? UserId { get; set; }


    public static explicit operator NoteTask(TaskRequestDto dto)
    {
        if (dto is not null) return new NoteTask
        {
            Title = dto.Title,
            Description = dto.Description!,
            CreatedAt = DateTime.Parse(dto.CreatedAt!),
            IsCompleted = (bool)dto.IsCompleted!,
            IsCanceled = false,
            UserId = dto.UserId
        };

        return default!;
    }
}
