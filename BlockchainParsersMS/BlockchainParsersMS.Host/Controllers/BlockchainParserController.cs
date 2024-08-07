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
            //ToDo:
            // 1. отправить адресс в сервис занимающийся парсингом 
            // 2. получить кол-во токенов в родительском токене 
            // 3. получить список токенов по типу кошелька с микросервиса 
            // 4. получить баланс на каждом токене
            // 5. в результирующую коллекцию добавить только токены у которых баланс больше 0
            // 6. отправить результат пользователю 
            throw new NotImplementedException();
        }
    }
}
