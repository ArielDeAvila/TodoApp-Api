using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Todo.Server.DTO;
using Todo.Server.Services.Interfaces;
using Todo.Server.Tools;

namespace Todo.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [Authorize]
    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAllByUser()
    {
        var uniqueNameclaim = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.UniqueName);

        if (uniqueNameclaim is null) return BadRequest(new BaseResponse<bool>
        {
            Success = false,
            Message = ReplyMessage.MESSAGE_FAILED
        });

        int userId = int.Parse(uniqueNameclaim.Value);

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

    [Authorize]
    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateTask([FromBody] TaskRequestDto task)
    {
        var uniqueNameclaim = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.UniqueName);

        if (uniqueNameclaim is null) return BadRequest(new BaseResponse<bool>
        {
            Success = false,
            Message = ReplyMessage.MESSAGE_FAILED
        });

        int userId = int.Parse(uniqueNameclaim.Value);

        task.UserId = userId;

        var response = await _taskService.CreateTask(task);

        if (response.Success)
        {
            return Ok(response);
        }
        else
        {
            return BadRequest(response);
        }
    }

    [Authorize]
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

    [Authorize]
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
