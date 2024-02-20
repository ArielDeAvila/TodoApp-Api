using FluentValidation;
using Todo.Server.DTO;

namespace Todo.Server.Validations;

public class TaskRequestValidation : AbstractValidator<TaskRequestDto>
{
    public TaskRequestValidation()
    {
        RuleFor(t => t.Title).NotEmpty();
        RuleFor(t => t.Description).NotEmpty().NotNull();
        RuleFor(t => t.CreatedAt).NotEmpty().NotNull();
        RuleFor(t => t.IsCompleted).NotNull();
        RuleFor(t => t.UserId).NotNull();
    }
}
