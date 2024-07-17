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
            throw new NotImplementedException();
        }

        public async Task<WalletTypeDTO> AddNewWalletTypeFromDTO(AddWalletTypeDTO addWalletTypeDTO)
        {
            WalletType newWalletType = addWalletTypeDTO.Adapt<WalletType>();

            WalletType addedWalletType = await _tokensDataLayer.AddWalletType(newWalletType);

            return addedWalletType.Adapt<WalletTypeDTO>();
        }

        public Task<List<TokenDTO>> GetAllTokens()
        {
            throw new NotImplementedException();
        }

        public Task<List<TokenDTO>> GetAllTokensByWalletType(string walletType)
        {
            throw new NotImplementedException();
        }
    }
}
