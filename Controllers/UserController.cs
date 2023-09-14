using Microsoft.AspNetCore.Mvc;
using SimpleBanking.Aplication;

namespace SimpleBanking.Controllers
{
    [ApiController]
    [Route("")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;

            ExecuteDDL.Execute();
        }

        [HttpPost("Create")]
        public ActionResult Post([FromBody] CreateUserRequest user)
        {   
            var result = CreateUser.Execute(user);
            if(result.Success == false)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPost("List")]
        public ActionResult Get()
        {   
        return Ok(ListUsers.Execute());
        }
    }
}