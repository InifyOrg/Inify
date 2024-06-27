using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletsMS.Infrastructure.Domain.DbCtx;
using WalletsMS.Infrastructure.Domain.Entities;

namespace WalletsMS.Infrastructure.DataLayer
{
    public class WalletsDataLayer : IWalletsDataLayer
    {
        public async Task<Wallet> AddWallet(Wallet newWallet, long walletTypeId)
        {
            using(WalletsMsDbContext db = new WalletsMsDbContext())
            {
                newWallet.WalletType = db.WalletTypes.Where(x => x.Id == walletTypeId).FirstOrDefault();

                if (newWallet.WalletType == null) return new Wallet();
                
                db.Wallet.Add(newWallet);

                await db.SaveChangesAsync();

                return newWallet;
            }
        }

        public async Task<WalletType> AddWalletType(WalletType newWalletType)
        {
            using (WalletsMsDbContext db = new WalletsMsDbContext())
            {
                db.WalletTypes.Add(newWalletType);
                await db.SaveChangesAsync();

                return newWalletType;
            }
        }

        public async Task<bool> DeleteWalletById(long id)
        {
            using (WalletsMsDbContext db = new WalletsMsDbContext())
            {
                Wallet wallet = await db.Wallet.FirstOrDefaultAsync(x => x.Id == id);

                db.Wallet.Remove(wallet);

                await db.SaveChangesAsync();

                return true;
            }
        }

        public async Task<Wallet> GetWalletById(long userId)
        {
            using (WalletsMsDbContext db = new WalletsMsDbContext())
            {
                Wallet walletById = await db.Wallet.FirstOrDefaultAsync(x => x.UserId == userId);

                return walletById != null ? walletById : new Wallet();
            }
        }
    }
}
