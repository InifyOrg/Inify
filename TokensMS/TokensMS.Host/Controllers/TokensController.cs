using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TokensMS.Contract;

namespace TokensMS.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllTokens()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{walletType}")]
        public async Task<IActionResult> GetAllTokensByWalletType(string walletType)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewToken([FromBody] AddTokenDTO addTokenDTO)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewPlatform([FromBody] AddPlatformDTO addPlatformDTO)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewWalletType([FromBody] AddWalletTypeDTO addWalletTypeDTO)
        {
            throw new NotImplementedException();
        }
    }
}
