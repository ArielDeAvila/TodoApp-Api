using Todo.Server.DTO;
using Todo.Server.Tools;

namespace Todo.Server.Services.Interfaces;

public interface IUserService
{
    Task<BaseResponse<bool>> Create(UserRequestDto dto);
}
