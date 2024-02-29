using FluentValidation;
using Todo.Server.Data.Entities;
using Todo.Server.DTO;
using Todo.Server.Services.Interfaces;
using Todo.Server.Tools;
using Todo.Server.UnitOfWork;

namespace Todo.Server.Services;

public class TaskService : ITaskService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<TaskRequestDto> _validator;

    public TaskService(IUnitOfWork unitOfWork, IValidator<TaskRequestDto> validator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<BaseResponse<IEnumerable<TaskResponseDto>?>> GetAllTasks(int userId)
    {
        var response = new BaseResponse<IEnumerable<TaskResponseDto>?>();

        var tasks = await _unitOfWork.TaskRepository.GetAll(userId);

        if (tasks is not null)
        {
            var tasksDto = new List<TaskResponseDto>();
            foreach (var task in tasks) tasksDto.Add((TaskResponseDto)task);

            response.Success = true;
            response.Data = tasksDto;
            response.Message = ReplyMessage.MESSAGE_QUERY;

        }
        else
        {
            response.Success = false;
            response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
        }

        return response;

    }

    public async Task<BaseResponse<bool>> CreateTask(TaskRequestDto taskDto, int userId)
    {
        var response = new BaseResponse<bool>();
        var validate = await _validator.ValidateAsync(taskDto);

        if (!validate.IsValid)
        {
            response.Success = false;
            response.Message = ReplyMessage.MESSAGE_VALIDATE;

            return response;
        }

        NoteTask task = TodoMapper.MapTask(taskDto);

        task.UserId = userId;

        _unitOfWork.TaskRepository.Create(task);

        var save = await _unitOfWork.CommitAsync();

        response.Data = save > 0;

        if (response.Data)
        {
            response.Success = true;
            response.Message = ReplyMessage.MESSAGE_SAVE;
        }
        else
        {
            response.Success = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
        }

        return response;

    }

    public async Task<BaseResponse<bool>> CompleteTask(int id)
    {
        var response = new BaseResponse<bool>();

        var task = await _unitOfWork.TaskRepository.GetById(id);

        if (task is null)
        {
            response.Success = false;
            response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

            return response;
        }

        task.IsCompleted = true;

        _unitOfWork.TaskRepository.Update(task);

        var save = await _unitOfWork.CommitAsync();

        response.Data = save > 0;

        if (response.Data)
        {
            response.Success = true;
            response.Message = ReplyMessage.MESSAGE_UPDATE;
        }
        else
        {
            response.Success = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
        }

        return response;

    }

    public async Task<BaseResponse<bool>> DeleteTask(int id)
    {
        var response = new BaseResponse<bool>();

        var task = await _unitOfWork.TaskRepository.GetById(id);

        if (task is null)
        {
            response.Success = false;
            response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

            return response;
        }

        task.IsCanceled = true;

        _unitOfWork.TaskRepository.Update(task);

        var save = await _unitOfWork.CommitAsync();

        response.Data = save > 0;

        if (response.Data)
        {
            response.Success = true;
            response.Message = ReplyMessage.MESSAGE_DELETE;
        }
        else
        {
            response.Success = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
        }

        return response;
    }
}
