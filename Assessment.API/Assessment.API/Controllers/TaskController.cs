using Assessment.DataAccess.Data.Repository.IRepository;
using Assessment.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Assessment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class TaskController : Controller
    {
        private readonly ITaskRepository _taskRepository;
        
        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository= taskRepository;    
        }
        // GET: api/<TaskController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult  Get()
        {
            return Ok(_taskRepository.GetAll());
        }

        // GET api/<TaskController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int id)
        {
            return Ok(_taskRepository.GetById(id));
        }

        // POST api/<TaskController>
        [HttpPost("Post")]
     
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(nameof(Post), OperationId = nameof(Post))]
        public IActionResult Post(TaskModel value)
        {
            _taskRepository.Insert(value);
            _taskRepository.Save();

            return Ok();
        }

        // PUT api/<TaskController>/5
        [HttpPut("Put")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(nameof(Put), OperationId = nameof(Put))]
        public IActionResult Put(TaskModel value)
        {
            _taskRepository.Update(value);
            _taskRepository.Save();
            return Ok();
        }

        // DELETE api/<TaskController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(nameof(Delete), OperationId = nameof(Delete))]
        public IActionResult Delete(int id)
        {
            _taskRepository.Delete(id);
            _taskRepository.Save();
            return Ok();
        }
    }
}
