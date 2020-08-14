using Microsoft.AspNetCore.Mvc;
using NetCore.FluentValidationPrototype.App.Dtos;

namespace NetCore.FluentValidationPrototype.App.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public sealed class FluentValidationController : ControllerBase
    {
        [HttpPost]
        [Route("api/fluent-validation/test")]
        public IActionResult Test([FromBody] WithoutAttributeDto dto)
        {
            return Ok(dto);
        }

        [HttpPost]
        [Route("api/fluent-validation/test-mixed")]
        public IActionResult TestMixed([FromBody] MixedDto dto)
        {
            return Ok(dto);
        }
    }
}
