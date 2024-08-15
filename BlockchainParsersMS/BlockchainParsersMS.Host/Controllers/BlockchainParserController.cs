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
            // ToDo:
            //1. получить кошельки этого пользователя (в том микросервисе есть уже проверка на наличие пользователя)
            //2. получить базовый токен на каждом кошельке
            //3. получить список токенов для парсинга
            //4. получить количество денег в каждом токене для каждого кошелька
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
