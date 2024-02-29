using Todo.Server.Data.Entities;

namespace Todo.Server.DTO;

public class TaskRequestDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool? IsCompleted { get; set; }
    public string? CreatedAt { get; set; }

}
