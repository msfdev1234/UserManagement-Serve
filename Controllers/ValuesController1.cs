using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UserManagement_Serv.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class ValuesController1 : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello world!");
        }
    }
}
