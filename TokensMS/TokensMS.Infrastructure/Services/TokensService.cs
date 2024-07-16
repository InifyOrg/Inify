using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokensMS.Contract;

namespace TokensMS.Infrastructure.Services
{
    public class TokensService : ITokensService
    {
        public Task<PlatformDTO> AddNewPlatformFromDTO(AddPlatformDTO addPlatformDTO)
        {
            throw new NotImplementedException();
        }

        public Task<TokenDTO> AddNewTokenFromDTO(AddTokenDTO addTokenDTO)
        {
            throw new NotImplementedException();
        }

        public Task<WalletTypeDTO> AddNewWalletTypeFromDTO(AddWalletTypeDTO addWalletTypeDTO)
        {
            throw new NotImplementedException();
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
