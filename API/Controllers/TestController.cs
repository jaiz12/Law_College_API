using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {

        [HttpGet]
        public async Task<object> GetTest()
        {
            return Ok("OK");
        }
    }
}
