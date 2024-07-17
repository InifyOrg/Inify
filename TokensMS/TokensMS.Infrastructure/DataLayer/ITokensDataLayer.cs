using TokensMS.Contract;
using TokensMS.Infrastructure.Domain.Entities;

namespace TokensMS.Infrastructure
{
    public interface ITokensDataLayer
    {
        Task<Platform> AddPlatform(Platform addPlatformDTO);
        Task<WalletType> AddWalletType(WalletType newWalletType);
    }
}