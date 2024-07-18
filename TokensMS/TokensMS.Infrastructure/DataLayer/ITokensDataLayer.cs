using TokensMS.Contract;
using TokensMS.Infrastructure.Domain.Entities;

namespace TokensMS.Infrastructure
{
    public interface ITokensDataLayer
    {
        Task<Platform> AddPlatform(Platform addPlatformDTO);
        Task<Token> AddToken(Token newToken);
        Task<WalletType> AddWalletType(WalletType newWalletType);
        Task<List<Token>> GetAll();
        Task<List<Token>> GetAllByWalletType(string walletType);
    }
}