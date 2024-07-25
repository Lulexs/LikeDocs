using Application.Core;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class BaseApiController : ControllerBase {
    protected ActionResult HandleResult<T>(Result<T> result) {
        Console.WriteLine(result.Error);
        if (result.IsSuccess && result.Value != null) {
            return Ok(result.Value);
        }
        else if (result.IsSuccess && result.Value == null) {
            return NotFound(result.Error);
        }
        return BadRequest(result.Error);
    }
}