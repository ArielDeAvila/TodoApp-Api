using FluentValidation;
using Todo.Server.Data.Entities;
using Todo.Server.DTO;
using Todo.Server.Services.Interfaces;
using Todo.Server.Tools;
using Todo.Server.UnitOfWork;
using BC = BCrypt.Net.BCrypt;

namespace Todo.Server.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UserRequestDto> _validator;

    public UserService(IUnitOfWork unitOfWork, IValidator<UserRequestDto> validator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<BaseResponse<bool>> Create(UserRequestDto dto)
    {
        var response = new BaseResponse<bool>();
        var validate = await _validator.ValidateAsync(dto);
        
        if(!validate.IsValid)
        {
            response.Success = false;
            response.Message = ReplyMessage.MESSAGE_VALIDATE;
            response.Errors = validate.Errors;

            return response;
        }

        var existingUser = await _unitOfWork.UserRepository.GetUserByEmail(dto.Email!);

        if (existingUser is not null)
        {
            response.Success = false;
            response.Message = ReplyMessage.MESSAGE_EXISTS_EMAIL;

            return response;
        }

        User user = TodoMapper.MapUser(dto);
        user.Password = BC.HashPassword(user.Password);

        _unitOfWork.UserRepository.Create(user);

        var created = await _unitOfWork.CommitAsync();

        response.Data = created > 0;

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
}
