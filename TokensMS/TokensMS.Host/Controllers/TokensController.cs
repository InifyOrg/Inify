﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TokensMS.Contract;
using TokensMS.Infrastructure;

namespace TokensMS.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly ITokensService _tokensService;

        public TokensController(ITokensService tokensService)
        {
            _tokensService = tokensService;
        }


        [HttpGet("getAllTokens")]
        public async Task<IActionResult> GetAllTokens()
        {
            List<TokenDTO> tokens = await _tokensService.GetAllTokens();

            if(tokens.Count < 1) 
                return NotFound();
            return Ok(tokens);
        }

        [HttpGet("getAllTokensByWalletType/{walletType}")]
        public async Task<IActionResult> GetAllTokensByWalletType(string walletType)
        {
            List<TokenDTO> tokens = await _tokensService.GetAllTokensByWalletType(walletType);

            if (tokens.Count < 1)
                return NotFound();
            return Ok(tokens);
        }

        [HttpPost("addNewToken")]
        public async Task<IActionResult> AddNewToken([FromBody] AddTokenDTO addTokenDTO)
        {
            TokenDTO tokenDto = await _tokensService.AddNewTokenFromDTO(addTokenDTO);

            if (tokenDto.Id < 1)
                return NotFound();
            return Ok(tokenDto);
        }

        [HttpPost("addNewPlatform")]
        public async Task<IActionResult> AddNewPlatform([FromBody] AddPlatformDTO addPlatformDTO)
        {
            PlatformDTO platformFromDto = await _tokensService.AddNewPlatformFromDTO(addPlatformDTO);

            if (platformFromDto.Id < 1)
                return NotFound();
            return Ok(platformFromDto);
        }

        [HttpPost("addNewWalletType")]
        public async Task<IActionResult> AddNewWalletType([FromBody] AddWalletTypeDTO addWalletTypeDTO)
        {
            WalletTypeDTO walletTypeFromDto = await _tokensService.AddNewWalletTypeFromDTO(addWalletTypeDTO);

            if (walletTypeFromDto.Id < 1)
                return NotFound();
            return Ok(walletTypeFromDto);
        }
    }
}
