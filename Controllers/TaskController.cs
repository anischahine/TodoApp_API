using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TodoApp_API.Models;
using TodoApp_API.Services;


namespace TodoApp_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskService _taskService;

        public TaskController(MongoDBService mongoDBService)
        {
            mongoDBService.GetTaskCollection();
            _taskService = mongoDBService._taskService;
        }

        //Getting all tasks info
        [HttpGet, Route("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<List<Tasks>> GetAllTasks() =>
            await _taskService.GetAsync();

        //getting tasks for specific account by email and by state
        [HttpGet("getByEmailState/{email}/{state}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Tasks>>> GetTasksByEmail([FromRoute(Name = "email")] string email, [FromRoute(Name = "state")] string state)
        {
            //checking if that account has tasks..
            var task = await _taskService.GetAsync(email, state);
            if (task is null)
            {
                return NotFound();//account not having tasks..
            }
            return task;//returning list of tasks
        }

        //Create new task
        [HttpPost, Route("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateNewTask(Tasks newTask)
        {
            await _taskService.CreateAsync(newTask);
            return CreatedAtAction(nameof(CreateNewTask), new { id = newTask._Id }, newTask);
        }

        [HttpPut("update/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateExistingTask([FromRoute(Name = "id")] string id, Tasks updatedTask)
        {
            var task = await _taskService.GetAsync(id);
            if (task is null)
            {
                return NotFound();
            }
            updatedTask._Id = task._Id;
            await _taskService.UpdateAsync(id, updatedTask);
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteExistingTask([FromRoute(Name = "id")] string id)
        {
            var task = await _taskService.GetAsync(id);

            if (task is null)
            {
                return NotFound();
            }

            await _taskService.RemoveAsync(id);

            return NoContent();
        }
    }
}