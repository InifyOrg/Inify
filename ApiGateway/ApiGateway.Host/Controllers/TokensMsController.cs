using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TokensMs.Client;
using TokensMS.Contract;
using UsersMS.Client;

namespace ApiGateway.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensMsController : ControllerBase
    {
        private readonly ITokensMsClient _tokensMsClient;

        public TokensMsController(ITokensMsClient tokensMsClient)
        {
            _tokensMsClient = tokensMsClient;
        }

        [HttpGet("getAllTokens")]
        public async Task<IActionResult> GetAllTokens()
        {
            List<TokenDTO> tokens = await _tokensMsClient.GetAllTokens();

            if (tokens.Count < 1)
                return NotFound();
            return Ok(tokens);
        }

        [HttpGet("getAllTokensByWalletType/{walletType}")]
        public async Task<IActionResult> GetAllTokensByWalletType(string walletType)
        {
            List<TokenDTO> tokens = await _tokensMsClient.GetAllTokensByWalletType(walletType);

            if (tokens.Count < 1)
                return NotFound();
            return Ok(tokens);
        }


    }
}
