using FluentValidation.Results;

namespace Todo.Server.Tools;

public class BaseResponse<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public string Message { get; set; } = null!;
    public IEnumerable<ValidationFailure>? Errors { get; set; }
}
