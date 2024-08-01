using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokensMS.Contract;

namespace TokensMs.Client
{
    public interface ITokensMsClient
    {
        Task<List<TokenDTO>> GetAllTokens();
        Task<List<TokenDTO>> GetAllTokensByWalletType(string walletType);
    }
}
