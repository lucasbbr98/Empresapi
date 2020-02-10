using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Empresapi.Controllers
{
    public class DatabaseController : BaseController
    {
        [HttpPut("company/{id}")]
        public async Task<IActionResult> Company([FromRoute] string path)
        {
            return Ok();
        }
    }
}