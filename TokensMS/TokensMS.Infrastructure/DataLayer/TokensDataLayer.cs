using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokensMS.Infrastructure.Domain.Entities;

namespace TokensMS.Infrastructure.DataLayer
{
    public class TokensDataLayer : ITokensDataLayer
    {
        public Task<Platform> AddPlatform(Platform addPlatformDTO)
        {
            throw new NotImplementedException();
        }

        public Task<WalletType> AddWalletType(WalletType newWalletType)
        {
            throw new NotImplementedException();
        }
    }
}
