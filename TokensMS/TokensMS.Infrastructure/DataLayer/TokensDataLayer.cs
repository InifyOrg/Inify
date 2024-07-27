using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokensMS.Infrastructure.Domain.DbCtx;
using TokensMS.Infrastructure.Domain.Entities;

namespace TokensMS.Infrastructure.DataLayer
{
    public class TokensDataLayer : ITokensDataLayer
    {
        public async Task<Platform> AddPlatform(Platform addPlatformDTO)
        {
            using (TokensMsDbContext db = new TokensMsDbContext())
            {
                db.Platforms.Add(addPlatformDTO);
                await db.SaveChangesAsync();

                return addPlatformDTO;
            }
        }

        public Task<List<Token>> AddRangeTokens(List<Token> newTokens)
        {

            return Task.FromResult( newTokens);
        }

        public async Task<Token> AddToken(Token newToken)
        {
            using (TokensMsDbContext db = new TokensMsDbContext())
            {
                newToken.WalletType = await db.WalletTypes.Where(wt => wt.Title == newToken.WalletType.Title).FirstOrDefaultAsync();
                newToken.Platform = await db.Platforms.Where(wt => wt.Slug == newToken.Platform.Slug).FirstOrDefaultAsync();
                db.Tokens.Add(newToken);
                await db.SaveChangesAsync();

                return newToken;
            }
        }

        public async Task<WalletType> AddWalletType(WalletType newWalletType)
        {
            using (TokensMsDbContext db = new TokensMsDbContext())
            {
                db.WalletTypes.Add(newWalletType);
                await db.SaveChangesAsync();

                return newWalletType;
            }
        }

        public async Task<List<Token>> GetAll()
        {
            using (TokensMsDbContext db = new TokensMsDbContext())
            {
                List<Token> tokens = await db.Tokens.Include(t => t.Platform).Include(t => t.WalletType).ToListAsync();

                return tokens != null ? tokens : new List<Token>();
            }
        }

        public async Task<List<Token>> GetAllByWalletType(string walletType)
        {
            using (TokensMsDbContext db = new TokensMsDbContext())
            {
                List<Token> tokens = await db.Tokens.Where(t => t.WalletType.Title == walletType).Include(t => t.Platform).Include(t => t.WalletType).ToListAsync();

                return tokens != null ? tokens : new List<Token>();
            }
        }

        public Task<List<Platform>> GetAllPlatforms()
        {
            throw new NotImplementedException();
        }
    }
}
