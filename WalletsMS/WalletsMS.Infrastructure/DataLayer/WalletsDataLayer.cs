using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
                newWallet.WalletType = await db.WalletTypes.Where(x => x.Id == walletTypeId).FirstOrDefaultAsync();

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

        public async Task<List<Wallet>> GetWalletsById(long userId)
        {
            using (WalletsMsDbContext db = new WalletsMsDbContext())
            {
                List<Wallet> walletsById = (from w in db.Wallet 
                                           join wt in db.WalletTypes 
                                           on w.WalletType.Id equals wt.Id
                                           where w.UserId == userId 
                                           select new Wallet { 
                                               Id = w.Id, 
                                               Address = w.Address, 
                                               WalletType = new WalletType 
                                               { 
                                                   Id = wt.Id, 
                                                   Title = wt.Title 
                                               }, 
                                               UserId = w.UserId 
                                           }).ToList();

                return walletsById != null ? walletsById : new List<Wallet>();
            }
        }
    }
}
