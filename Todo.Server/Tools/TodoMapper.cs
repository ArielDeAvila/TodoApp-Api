using Todo.Server.Data.Entities;
using Todo.Server.DTO;

namespace Todo.Server.Tools;

public static class TodoMapper
{
    public static User MapUser(UserRequestDto user)
    {
        return new User
        {
            UserName = user.UserName!,
            Password = user.Password!,
            Email = user.Email!,
            CreatedAt = DateTime.UtcNow
        };
    }
}
