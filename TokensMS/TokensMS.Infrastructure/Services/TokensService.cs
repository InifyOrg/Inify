using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokensMS.Contract;
using TokensMS.Infrastructure.Domain.Entities;

namespace TokensMS.Infrastructure.Services
{
    public class TokensService : ITokensService
    {
        private readonly ITokensDataLayer _tokensDataLayer;
        private readonly IWeb3Service _web3Service;
        public TokensService(ITokensDataLayer tokensDataLayer, IWeb3Service web3Service)
        {
            _tokensDataLayer = tokensDataLayer;
            _web3Service = web3Service;
        }

        public async Task<PlatformDTO> AddNewPlatformFromDTO(AddPlatformDTO addPlatformDTO)
        {
            Platform newPlatform = addPlatformDTO.Adapt<Platform>();

            Platform addedPlatform = await _tokensDataLayer.AddPlatform(newPlatform);

            return addedPlatform.Adapt<PlatformDTO>();
        }

        public async Task<TokenDTO> AddNewTokenFromDTO(AddTokenDTO addTokenDTO)
        {
            Token newToken = addTokenDTO.Adapt<Token>();

            newToken.Decimals = await _web3Service.GetDecimalsByAddress(newToken.Address);

            Token addedToken = await _tokensDataLayer.AddToken(newToken);

            return addedToken.Adapt<TokenDTO>();
        }

        public async Task<WalletTypeDTO> AddNewWalletTypeFromDTO(AddWalletTypeDTO addWalletTypeDTO)
        {
            WalletType newWalletType = addWalletTypeDTO.Adapt<WalletType>();

            WalletType addedWalletType = await _tokensDataLayer.AddWalletType(newWalletType);

            return addedWalletType.Adapt<WalletTypeDTO>();
        }

        public async Task<List<TokenDTO>> GetAllTokens()
        {
            List<Token> tokens = await _tokensDataLayer.GetAll();

            return tokens.Adapt<List<TokenDTO>>();
        }

        public async Task<List<TokenDTO>> GetAllTokensByWalletType(string walletType)
        {
            List<Token> tokens = await _tokensDataLayer.GetAllByWalletType(walletType);

            return tokens.Adapt<List<TokenDTO>>();
        }
    }
}
