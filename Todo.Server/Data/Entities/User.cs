using System.ComponentModel.DataAnnotations;

namespace Todo.Server.Data.Entities;

public class User : BaseEntity
{
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;

    public virtual ICollection<NoteTask> Tasks { get; set; } = new List<NoteTask>();
}
