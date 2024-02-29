using Todo.Server.DTO;
using Todo.Server.Tools;

namespace Todo.Server.Services.Interfaces;

public interface ITaskService
{
    Task<BaseResponse<IEnumerable<TaskResponseDto>?>> GetAllTasks(int userId);
    Task<BaseResponse<bool>> CreateTask(TaskRequestDto task, int userId);
    Task<BaseResponse<bool>> CompleteTask(int id);
    Task<BaseResponse<bool>> DeleteTask(int id);    
}
