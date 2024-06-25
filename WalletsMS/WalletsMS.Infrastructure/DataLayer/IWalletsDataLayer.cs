﻿using WalletsMS.Infrastructure.Domain.Entities;

namespace WalletsMS.Infrastructure
{
    public interface IWalletsDataLayer
    {
        Task<Wallet> AddWallet(Wallet newWallet);
        Task<WalletType> AddWalletType(WalletType newWalletType);
        Task<bool> DeleteWalletById(long id);
        Task<Wallet> GetWalletById(long userId);
    }
}