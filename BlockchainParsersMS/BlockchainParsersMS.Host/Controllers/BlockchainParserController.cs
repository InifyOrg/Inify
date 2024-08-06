using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlockchainParsersMS.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockchainParserController : ControllerBase
    {

        [HttpGet("parseManyByUserId/{userId}")]
        public async Task<IActionResult> parseManyByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("parseOneByAddress/{address}")]
        public async Task<IActionResult> parseOneByAddress(string address)
        {
            throw new NotImplementedException();
        }
    }
}
