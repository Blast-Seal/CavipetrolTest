using Azure;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Text.Json;

namespace CavipetrolTestBack.API.Controllers.Bases
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        protected IActionResult ResponseApi(int code, object obj, bool pascal = true)
        {
            if (obj == null)
            {
                return StatusCode(code);
            }
            Response.StatusCode = code;
            return new JsonResult(obj, new JsonSerializerOptions
            {
                PropertyNamingPolicy = pascal ? null : JsonNamingPolicy.CamelCase,
            });
        }

        protected IActionResult ResponseApiOk(object _object)
        {
            return ResponseApi(StatusCodes.Status200OK, _object);
        }
    }
}
