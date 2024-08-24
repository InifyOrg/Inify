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
        public async Task<IActionResult> ParseManyByUserId(long userId)
        {
            ParsingOutputDTO results = await _blockchainParserService.ParseManyByUserId(userId);
            if(results.Wallets.Count < 1) 
                return NotFound();
            return Ok(results);
        }

        [HttpGet("parseOneByAddress")]
        public async Task<IActionResult> ParseOneByAddress([FromQuery] WalletDTO walletInfo)
        {
            List<ParsedTokenDTO> parsedTokens = await _blockchainParserService.ParseOneByAddress(walletInfo);

            if (parsedTokens.Count < 1)
                return NotFound();

            ParsingOutputDTO results = new ParsingOutputDTO()
            {
                TotalBalance = _blockchainParserService.GetTotalBalance(parsedTokens),
                TotalBestTokenSymbol = _blockchainParserService.GetTotalBestSymbol(parsedTokens).Symbol,
            };

            results.Wallets = new List<WalletParsedInfoDTO>()
            {
                new WalletParsedInfoDTO() {
                    Wallet = walletInfo,
                    Balance = results.TotalBalance, 
                    Tokens = parsedTokens,
                }
            };

            results.Wallets[0].BestToken = _blockchainParserService.GetTotalBestSymbol(parsedTokens);

            return Ok(results);
        }
    }
}
