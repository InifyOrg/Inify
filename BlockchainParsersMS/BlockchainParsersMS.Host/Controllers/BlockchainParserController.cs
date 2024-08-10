using BlockchainParsersMS.Contract;
using BlockchainParsersMS.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlockchainParsersMS.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockchainParserController : ControllerBase
    {
        private readonly IBlockchainParserService _blockchainParserService;

        public BlockchainParserController(IBlockchainParserService blockchainParserService)
        {
            _blockchainParserService = blockchainParserService;
        }

        [HttpGet("parseManyByUserId/{userId}")]
        public async Task<IActionResult> parseManyByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("parseOneByAddress")]
        public async Task<IActionResult> parseOneByAddress([FromQuery] WalletInfoDTO walletInfo)
        {
            List<ParsedTokenDTO> parsedTokens = await _blockchainParserService.parseOneByAddress(walletInfo);

            if(parsedTokens.Count < 1)
                return NotFound();
            return Ok(parsedTokens);
        }
    }
}
