using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Services;
using static TaskFlow.Core.DTOs.TaskRequestDtos;
using static TaskFlow.Core.DTOs.TaskResponseDtos;

namespace TaskFlow.WebAPI.Controllers
{
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
        [HttpPost("GetTaskByGuid")]
        public async Task<IActionResult> GetTaskByGuidId(
            TaskGetByGuidRequestDto taskGetByGuidRequestDto
        )
        {
            var res = await _taskService.GetTaskByGuidId(taskGetByGuidRequestDto);
            return Ok(res);
        }

        [Authorize]
        [HttpPost("GetAllTaskForUser")]
        public async Task<IEnumerable<TaskGetResponseDto>> GetAllTaskByAuthorId(
            TaskGetAllForUserRequestDto taskGetAllForUserRequestDto
        )
        {
            var res = await _taskService.GetAllTaskByAuthorId(
                taskGetAllForUserRequestDto.UserGuidId
            );
            return (IEnumerable<TaskGetResponseDto>)res;
        }

        [Authorize]
        [HttpPost("AddTask")]
        public async Task<IActionResult> AddTask(TaskAddRequestDto addRequestDto)
        {
            var res = await _taskService.AddTask(addRequestDto);
            return Ok(res);
        }


        [Authorize]
        [HttpPost("UpdateTask")]
        public async Task<IActionResult> UpdateTask(TaskUpdateRequestDto updateRequestDto)
        {
            var res = await _taskService.UpdateTask(updateRequestDto);
            return Ok(res);
        }
    }
}