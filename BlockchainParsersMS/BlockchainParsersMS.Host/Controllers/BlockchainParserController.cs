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

        [HttpGet("parseOneByAddress/{address}")]
        public async Task<IActionResult> parseOneByAddress(string address)
        {
            //ToDo:
            // 2. получить кол-во токенов в родительском токене 
            // 3. получить список токенов по типу кошелька с микросервиса 
            // 4. получить баланс на каждом токене
            // 5. в результирующую коллекцию добавить только токены у которых баланс больше 0
            // 6. отправить результат пользователю 
            List<ParsedTokenDTO> parsedTokens = await _blockchainParserService.parseOneByAddress(address);

            if(parsedTokens.Count < 1)
                return NotFound();
            return Ok(parsedTokens);
        }
    }
}
