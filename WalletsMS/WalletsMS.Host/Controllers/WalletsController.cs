using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WalletsMS.Infrastructure;
using WalletsMS.Contract;

namespace WalletsMS.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletsController : ControllerBase
    {
        private readonly IWalletsService _walletsService;

        public WalletsController(IWalletsService walletsService)
        {
            _walletsService = walletsService;
        }

        [HttpGet("getAllWalletsByUserId/{userId}")]
        public async Task<IActionResult> GetAllWalletsByUserId(long userId)
        {
            List<WalletDTO> walletByUserId = await _walletsService.GetAllWalletsByUserId(userId);

            if(walletByUserId.Count < 1)
            {
                return NotFound();
            }

            return Ok(walletByUserId);
        }

        [HttpGet("getAllWalletTypes")]
        public async Task<IActionResult> GetAllWalletTypes()
        {
            List<WalletTypeDTO> walletTypes = await _walletsService.GetAllWalletTypes();

            if (walletTypes.Count < 1)
            {
                return NotFound();
            }

            return Ok(walletTypes);
        }


        [HttpPost("addNewWallet")]
        public async Task<IActionResult> AddNewWallet([FromBody] AddWalletDTO addWalletFromDTO)
        {
            WalletDTO walletFromDTO = await _walletsService.AddNewWalletFromDTO(addWalletFromDTO);

            if (walletFromDTO.Id < 1)
            {
                return NotFound();
            }

            return Ok(walletFromDTO);
        }

        [HttpPost("addNewWalletType")]
        public async Task<IActionResult> AddNewWalletType([FromBody] AddWalletTypeDTO addWalletTypeFromDTO)
        {
            WalletTypeDTO walletTypeFromDto = await _walletsService.AddNewWalletTypeFromDTO(addWalletTypeFromDTO);

            if (walletTypeFromDto.Id < 1)
            {
                return NotFound();
            }

            return Ok(walletTypeFromDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWallet(long id)
        {
            bool isDeleted = await _walletsService.DeleteWalletById(id);

            return Ok(isDeleted);
        }

    }
}
