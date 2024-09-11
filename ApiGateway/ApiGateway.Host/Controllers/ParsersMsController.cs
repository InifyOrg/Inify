using BlockchainParsersMS.Client;
using BlockchainParsersMS.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsersMS.Client;

namespace ApiGateway.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParsersMsController : ControllerBase
    {
        private readonly IBlockchainParserClient _parserClient;
        private readonly IUsersMsClient _usersMsClient;

        public ParsersMsController(IBlockchainParserClient parserClient, IUsersMsClient usersMsClient)
        {
            _parserClient = parserClient;
            _usersMsClient = usersMsClient;
        }

        [HttpGet("parseManyByUserId/{userId}")]
        public async Task<IActionResult> ParseManyByUserId([FromHeader] string Authorization, long userId)
        {
            bool isAuthorized = await _usersMsClient.ValidateAccessToken(Authorization);

            if (isAuthorized)
            {
                ParsingOutputDTO results = await _parserClient.parseManyByUserId(userId);
                if (results.Wallets.Count < 1)
                    return NotFound();
                return Ok(results);
            }
            return Unauthorized();
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
