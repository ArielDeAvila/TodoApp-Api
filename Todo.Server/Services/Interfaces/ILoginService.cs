using Todo.Server.DTO;
using Todo.Server.Tools;

namespace Todo.Server.Services.Interfaces;

public interface ILoginService
{
    Task<BaseResponse<string>> Login(LoginRequestDto request); 
}
