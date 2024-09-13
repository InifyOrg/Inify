using WalletsMS.Infrastructure.Domain.Entities;

namespace WalletsMS.Infrastructure
{
    public interface IWalletsDataLayer
    {
        Task<Wallet> AddWallet(Wallet newWallet, long walletTypeId);
        Task<WalletType> AddWalletType(WalletType newWalletType);
        Task<bool> DeleteWalletById(long id);
        Task<List<Wallet>> GetWalletsById(long userId);
        Task<List<WalletType>> GetWalletTypes();
    }
}