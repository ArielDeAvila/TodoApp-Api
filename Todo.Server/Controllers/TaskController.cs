using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Todo.Server.DTO;
using Todo.Server.Services.Interfaces;
using Todo.Server.Tools;

namespace Todo.Server.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAllByUser()
    {
        var uniqueNameclaim = User.Claims
                                .FirstOrDefault(c =>
                                c.Properties.Values.Contains(JwtRegisteredClaimNames.UniqueName));

        if (uniqueNameclaim is null) return BadRequest(new BaseResponse<bool>
        {
            Success = false,
            Message = ReplyMessage.MESSAGE_INVALID_TOKEN
        });

        int userId = int.Parse(uniqueNameclaim!.Value);

        var response = await _taskService.GetAllTasks(userId);

        if (response.Success)
        {
            return Ok(response);
        }
        else
        {
            return BadRequest(response);
        }
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateTask([FromBody] TaskRequestDto task)
    {
        var uniqueNameclaim = User.Claims
                                .FirstOrDefault(c =>
                                c.Properties.Values.Contains(JwtRegisteredClaimNames.UniqueName));

        if (uniqueNameclaim is null) return Unauthorized(new BaseResponse<bool>
        {
            Success = false,
            Message = ReplyMessage.MESSAGE_INVALID_TOKEN
        });

        int userId = int.Parse(uniqueNameclaim.Value);

        var response = await _taskService.CreateTask(task, userId);

        if (response.Success)
        {
            return Ok(response);
        }
        else
        {
            return BadRequest(response);
        }
    }

    [HttpPatch]
    [Route("Complete/id")]
    public async Task<IActionResult> CompleteTask(int id)
    {
        var response = await _taskService.CompleteTask(id);

        if (response.Success)
        {
            return Ok(response);
        }
        else
        {
            return BadRequest(response);
        }
    }

    [HttpDelete]
    [Route("Delete/id")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var response = await _taskService.DeleteTask(id);

        if(response.Success)
        {
            return Ok(response);
        }
        else
        {
            return BadRequest(response);
        }
    }

}
