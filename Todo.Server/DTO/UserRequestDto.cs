using Todo.Server.Data.Entities;

namespace Todo.Server.DTO;

public class UserRequestDto
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? VerifyPassword { get; set; }
    public string? Email { get; set; }

}
