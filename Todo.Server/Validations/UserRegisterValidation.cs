using FluentValidation;
using Todo.Server.DTO;

namespace Todo.Server.Validations;

public class UserRegisterValidation : AbstractValidator<UserRequestDto>
{
    public UserRegisterValidation()
    {
        RuleFor(u => u.Email).EmailAddress().NotEmpty().NotNull();
        RuleFor(u => u.UserName).NotEmpty().NotNull();
        RuleFor(u => u.Password).NotEmpty().NotNull();
        RuleFor(u => u.VerifyPassword).NotEmpty().NotNull().Equal(u => u.Password);
    }
}
