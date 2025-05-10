using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Core.Exceptions;
using TaskFlow.Helpers;
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
        [HttpPost("GetTaskById")]
        public async Task<IActionResult> GetTaskById(
            TaskGetByIdRequestDto taskGetByIdRequestDto)
        {
            var res = await _taskService.GetTaskById(taskGetByIdRequestDto);
            if (res == null)
            {
                throw new NotFoundException("Task not found");
            }

            return Ok(ApiResponse<TaskGetResponseDto>.SuccessResponse(res, "Task retrieved successfully"));
        }

        [Authorize]
        [HttpPost("AddTask")]
        public async Task<IActionResult> AddTask(
            TaskAddRequestDto addRequestDto)
        {
            var res = await _taskService.AddTask(addRequestDto);
            return Ok(ApiResponse<object>.SuccessResponse(res, "Task added successfully"));
        }

        [Authorize]
        [HttpPost("UpdateTask")]
        public async Task<IActionResult> UpdateTask(
            TaskUpdateRequestDto updateRequestDto)
        {
            var res = await _taskService.UpdateTask(updateRequestDto);
            return Ok(ApiResponse<object>.SuccessResponse(res, "Task updated successfully"));
        }

        [Authorize]
        [HttpGet("GetAllTaskForUser")]
        public async Task<IActionResult> GetAllTaskByAuthorId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized(ApiResponse<string>.ErrorResponse("", "User ID not found in token"));
            }

            var userId = int.Parse(userIdClaim.Value);
            var res = await _taskService.GetAllTaskByAuthorId(userId);
            return Ok(ApiResponse<IEnumerable<TaskGetResponseDto>>.SuccessResponse(res,
                "Tasks retrieved successfully"));
        }


        [HttpGet("GetTaskStatusToProject/{id}")]
        public async Task<ActionResult<TaskStatusResponseDto>> GetTaskStatusById(int id)
        {
            var result = await _taskService.GetTaskStatusByIdAsync(id);

            return Ok(result);
        }

        [HttpGet("GetAllTaskStatusByProjectId/{projectId}")]
        public async Task<ActionResult<IEnumerable<TaskStatusResponseDto>>> GetTaskStatusesByProjectId(int projectId)
        {
            var results = await _taskService.GetTaskStatusesByProjectIdAsync(projectId);
            return Ok(results);
        }

        [HttpPost("AddTaskStatusToProject")]
        public async Task<IActionResult> AddTaskStatus([FromBody] TaskStatusAddRequestDto dto)
        {
            var result = await _taskService.AddTaskStatusAsync(dto);
            if (!result)
                return BadRequest("Failed to add task status.");

            return Ok("Task status added successfully.");
        }

        [HttpPut("UpdateTaskStatusToProject")]
        public async Task<IActionResult> UpdateTaskStatus([FromBody] TaskStatusAddRequestDto taskStatusAddRequestDto)
        {
            var updated = await _taskService.UpdateTaskStatusAsync(taskStatusAddRequestDto);
            if (!updated)
                return NotFound("Task status not found or could not be updated.");

            return Ok("Task status updated successfully.");
        }

        [HttpDelete("DeleteTaskStatusToProject/{id}")]
        public async Task<IActionResult> DeleteTaskStatus(int id)
        {
            var deleted = await _taskService.DeleteTaskStatusAsync(id);
            if (!deleted)
                return NotFound("Task status not found or could not be deleted.");

            return Ok("Task status deleted successfully.");
        }
    }
}