using BlockchainParsersMS.Client;
using BlockchainParsersMS.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParsersMsController : ControllerBase
    {
        private readonly IBlockchainParserClient _parserClient;

        public ParsersMsController(IBlockchainParserClient parserClient)
        {
            _parserClient = parserClient;
        }

        [HttpGet("parseManyByUserId/{userId}")]
        public async Task<IActionResult> ParseManyByUserId(long userId)
        {
            ParsingOutputDTO results = await _parserClient.parseManyByUserId(userId);
            if (results.Wallets.Count < 1)
                return NotFound();
            return Ok(results);
        }

        [HttpGet("parseOneByAddress")]
        public async Task<IActionResult> ParseOneByAddress([FromQuery] WalletDTO walletInfo)
        {
            ParsingOutputDTO results = await _parserClient.parseOneByAddress(walletInfo);
            if (results.Wallets.Count < 1)
                return NotFound();
            return Ok(results);
        }

    }
}
