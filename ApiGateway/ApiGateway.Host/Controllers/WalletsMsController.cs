using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsersMS.Client;
using WalletsMS.Client;
using WalletsMS.Contract;

namespace ApiGateway.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletsMsController : ControllerBase
    {
        private readonly IWalletsMsClient _walletsMsClient;
        private readonly IUsersMsClient _usersMsClient;

        public WalletsMsController(IWalletsMsClient walletsMsClient, IUsersMsClient usersMsClient)
        {
            _walletsMsClient = walletsMsClient;
            _usersMsClient = usersMsClient;
        }

        [HttpGet("getAllWalletsByUserId/{userId}")]
        public async Task<IActionResult> GetAllWalletsByUserId([FromHeader] string Authorization, long userId)
        {
            bool isAuthorized = await _usersMsClient.ValidateAccessToken(Authorization);

            if (isAuthorized)
            {
                List<WalletDTO> walletByUserId = await _walletsMsClient.GetAllWalletsByUserId(userId);

                if (walletByUserId.Count < 1)
                {
                    return NotFound();
                }

                return Ok(walletByUserId);
            }
            return Unauthorized();

        }

        [HttpGet("getAllWalletTypes")]
        public async Task<IActionResult> GetAllWalletTypes([FromHeader] string Authorization)
        {
            bool isAuthorized = await _usersMsClient.ValidateAccessToken(Authorization);

            if (isAuthorized)
            {
                List<WalletTypeDTO> walletTypes = await _walletsMsClient.GetAllWalletTypes();

                if (walletTypes.Count < 1)
                {
                    return NotFound();
                }

                return Ok(walletTypes);
            }
            return Unauthorized();

        }

        [HttpPost("addNewWallet")]
        public async Task<IActionResult> AddNewWallet([FromHeader] string Authorization, [FromBody] AddWalletDTO addWalletFromDTO)
        {
            bool isAuthorized = await _usersMsClient.ValidateAccessToken(Authorization);

            if (isAuthorized)
            {
                WalletDTO walletFromDTO = await _walletsMsClient.AddNewWalletFromDTO(addWalletFromDTO);

                if (walletFromDTO.Id < 1)
                {
                    return NotFound();
                }

                return Ok(walletFromDTO);

            }
            return Unauthorized();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWallet([FromHeader] string Authorization, long id)
        {
            bool isAuthorized = await _usersMsClient.ValidateAccessToken(Authorization);

            if (isAuthorized)
            {
                bool isDeleted = await _walletsMsClient.DeleteWalletById(id);

                return Ok(isDeleted);
            }
            return Unauthorized();

        }


    }
}
