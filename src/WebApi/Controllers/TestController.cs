using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly SimpleOptions _simpleOptions;
        private readonly ApiOptions _apiOptions;
        public TestController(IOptions<SimpleOptions> simpleOptions, 
            IOptions<ApiOptions> apiOptions)
        {
            _simpleOptions = simpleOptions.Value;
            _apiOptions = apiOptions.Value;
        }

        [HttpPost]
        public IActionResult SendEmail()
        {
            return Ok();
        }
    }
}
