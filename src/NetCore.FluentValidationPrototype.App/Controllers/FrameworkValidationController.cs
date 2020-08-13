using Microsoft.AspNetCore.Mvc;
using NetCore.FluentValidationPrototype.App.Dtos;

namespace NetCore.FluentValidationPrototype.App.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public sealed class FrameworkValidationController : ControllerBase
    {
        [HttpPost]
        [Route("api/framework-validation/test")]
        public IActionResult Test([FromBody] WithAttributeDto dto)
        {
            return Ok(dto);
        }
    }
}
