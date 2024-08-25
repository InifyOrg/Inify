using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WalletsMS.Client;
using WalletsMS.Contract;

namespace ApiGateway.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletsMsController : ControllerBase
    {
        private readonly IWalletsMsClient _walletsMsClient;

        public WalletsMsController(IWalletsMsClient walletsMsClient)
        {
            _walletsMsClient = walletsMsClient;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAllWalletsByUserId(long userId)
        {
            List<WalletDTO> walletByUserId = await _walletsMsClient.GetAllWalletsByUserId(userId);

            if (walletByUserId.Count < 1)
            {
                return NotFound();
            }

            return Ok(walletByUserId);
        }

        [HttpPost("addNewWallet")]
        public async Task<IActionResult> AddNewWallet([FromBody] AddWalletDTO addWalletFromDTO)
        {
            WalletDTO walletFromDTO = await _walletsMsClient.AddNewWalletFromDTO(addWalletFromDTO);

            if (walletFromDTO.Id < 1)
            {
                return NotFound();
            }

            return Ok(walletFromDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWallet(long id)
        {
            bool isDeleted = await _walletsMsClient.DeleteWalletById(id);

            return Ok(isDeleted);
        }


    }
}
