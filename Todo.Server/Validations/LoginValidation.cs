using FluentValidation;
using Todo.Server.DTO;

namespace Todo.Server.Validations;

public class LoginValidation : AbstractValidator<LoginRequestDto>
{
    public LoginValidation()
    {
        RuleFor(l => l.Email).EmailAddress().NotEmpty().NotNull();
        RuleFor(l => l.Password).NotEmpty().NotNull();  
    }
}
