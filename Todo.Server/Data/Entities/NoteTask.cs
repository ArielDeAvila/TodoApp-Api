namespace Todo.Server.Data.Entities;

public class NoteTask : BaseEntity
{
    public string? Title { get; set; }
    public string Description { get; set; } = null!;
    public bool IsCompleted { get; set; } = false;
    public bool IsCanceled { get; set; } = false;
    
    
    public int? UserId { get; set; } 
    public virtual User? User { get; set; }

}
