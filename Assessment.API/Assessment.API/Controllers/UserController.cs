using Assessment.DataAccess.ApiKey;
using Assessment.DataAccess.ApiKey.IApiKey.IApiKey;
using Assessment.DataAccess.Data.Repository.IRepository;
using Assessment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Assessment.API.Controllers
{

    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserRepository _repository;
        private readonly IApiKeyService _apiKey;
        public UserController(IUserRepository userRepository, IApiKeyService apiKey)
        {
            this._repository = userRepository;
            this._apiKey=apiKey;    
        }
        [HttpGet]
        [Route("GenerateApiKey")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Index()
        {
            string response = _apiKey.GenerateApiKey();
            return Ok(response);
        }
        // GET: api/<UserController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(nameof(Get), OperationId = nameof(Get))]
        public IActionResult Get()
        {
            return Ok(_repository.GetAll());
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(nameof(Get), OperationId = nameof(Get))]
        public IActionResult Get(int id)
        {
            return Ok(_repository.GetById(id));
        }

        // POST api/<UserController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(nameof(Post), OperationId = nameof(Post))]
        public IActionResult Post(User value)
        {
            _repository.Insert(value);
            _repository.Save();
            return Ok();
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(nameof(Put), OperationId = nameof(Put))]
        public IActionResult Put(User value)
        {
            _repository.Insert(value);
            _repository.Save();
            return Ok();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(nameof(Delete), OperationId = nameof(Delete))]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            _repository.Save();
            return Ok();
        }
    }
}
