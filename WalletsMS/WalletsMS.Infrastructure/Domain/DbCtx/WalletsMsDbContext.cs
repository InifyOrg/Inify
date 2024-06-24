using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletsMS.Infrastructure.Domain.Entities;

namespace WalletsMS.Infrastructure.Domain.DbCtx
{
    public class WalletsMsDbContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("WalletsMsDb"));
        }

        public DbSet<WalletType> WalletTypes { get; set; }
        public DbSet<Wallet> Wallet { get; set; }

    }
}
