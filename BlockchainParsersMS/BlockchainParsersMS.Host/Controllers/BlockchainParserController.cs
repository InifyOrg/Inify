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
        public async Task<IActionResult> parseManyByUserId(long userId)
        {
            // ToDo:
            //1. получить кошельки этого пользователя (в том микросервисе есть уже проверка на наличие пользователя)
            //2. получить базовый токен на каждом кошельке
            //3. получить список токенов для парсинга
            //4. получить количество денег в каждом токене для каждого кошелька
            ParsingOutputDTO results = await _blockchainParserService.parseManyByUserId(userId);
            if(results.Wallets.Count < 1) 
                return NotFound();
            return Ok(results);
        }

        [HttpGet("parseOneByAddress")]
        public async Task<IActionResult> parseOneByAddress([FromQuery] WalletDTO walletInfo)
        {
            List<ParsedTokenDTO> parsedTokens = await _blockchainParserService.parseOneByAddress(walletInfo);

            if (parsedTokens.Count < 1)
                return NotFound();

            ParsingOutputDTO results = new ParsingOutputDTO()
            {
                TotalBalance = _blockchainParserService.getTotalBalance(parsedTokens),
                TotalBestTokenSymbol = _blockchainParserService.getTotalBestSymbol(parsedTokens).Symbol,
            };

            results.Wallets = new List<WalletParsedInfoDTO>()
            {
                new WalletParsedInfoDTO() {
                    Wallet = walletInfo,
                    Balance = results.TotalBalance, 
                    Tokens = parsedTokens,
                }
            };

            results.Wallets[0].BestToken = _blockchainParserService.getTotalBestSymbol(parsedTokens);

            return Ok(results);
        }
    }
}
